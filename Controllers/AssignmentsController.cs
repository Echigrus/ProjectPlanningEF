using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectPlanningEF.Models;

namespace ProjectPlanningEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public AssignmentsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Assignments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Assignment>>> GetAssignments()
        {
            return await _context.Assignments.ToListAsync();
        }

        // GET: api/Assignments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Assignment>> GetAssignment(Guid id)
        {
            var assignment = await _context.Assignments.FindAsync(id);

            if (assignment == null)
            {
                return NotFound();
            }

            return assignment;
        }

        // PUT: api/Assignments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAssignment(Guid id, Assignment assignment)
        {
            if (id != assignment.Id)
            {
                return BadRequest();
            }

            _context.Entry(assignment).State = EntityState.Modified;
            // Дни плана для предыдущей версии удаляются
            var linkedPTD = _context.PlanTable.Where(t => t.AssignmentId == assignment.Id);
            _context.PlanTable.RemoveRange(linkedPTD);

            try
            {
                await _context.SaveChangesAsync();
                _context.PlanTable.AddRange(GeneratePTDs(assignment));
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssignmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Assignments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Assignment>> PostAssignment(Assignment assignment)
        {
            _context.Assignments.Add(assignment);
            _context.PlanTable.AddRange(GeneratePTDs(assignment));
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAssignment), new { id = assignment.Id }, assignment);
        }

        // DELETE: api/Assignments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssignment(Guid id)
        {
            var assignment = await _context.Assignments.FindAsync(id);
            if (assignment == null)
            {
                return NotFound();
            }

            _context.Assignments.Remove(assignment);
            var linkedPTD = _context.PlanTable.Where(t => t.AssignmentId == assignment.Id);
            _context.PlanTable.RemoveRange(linkedPTD);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AssignmentExists(Guid id)
        {
            return _context.Assignments.Any(e => e.Id == id);
        }

        internal List<PlanTableDay> GeneratePTDs(Assignment assignment)
        {
            // Получаем ставки сотрудника, от новых к старым
            var workerRates = _context.WorkerHourlyRates.OrderByDescending(r => r.Start).ToList();
            // Соответствующие дни табеля, от новых к старым
            var timeTableDays = _context.TimeTable.Where(t =>
                t.WorkerId == assignment.WorkerId
                & t.Date >= assignment.Start
                & t.Date <= assignment.End
            ).OrderByDescending(t => t.Date);
            // Берём ставку для последнего (первого в сете) дня
            WorkerHourlyRate curRate = workerRates.First(r => r.Start <= timeTableDays.First().Date);
            List<PlanTableDay> ptd = new List<PlanTableDay>();
            foreach (var t in timeTableDays)
            {
                // Обновляем ставку, если её период прошёл
                if (curRate.Start > t.Date)
                {
                    curRate = workerRates.First(r => r.Start <= t.Date);
                }
                float ptdHours = t.Hours * assignment.LoadShare;
                ptd.Add(new PlanTableDay
                {
                    Date = t.Date,
                    AssignmentId = assignment.Id,
                    Hours = ptdHours,
                    Cost = ptdHours * curRate.HourlyRate
                });
            }
            return ptd;
        }
    }
}
