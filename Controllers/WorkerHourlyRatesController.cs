using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectPlanningEF.Models;

namespace ProjectPlanningEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerHourlyRatesController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public WorkerHourlyRatesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/WorkerHourlyRates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkerHourlyRate>>> GetWorkerHourlyRates()
        {
            return await _context.WorkerHourlyRates.ToListAsync();
        }

        // GET: api/WorkerHourlyRates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkerHourlyRate>> GetWorkerHourlyRate(Guid id)
        {
            var workerHourlyRate = await _context.WorkerHourlyRates.FindAsync(id);

            if (workerHourlyRate == null)
            {
                return NotFound();
            }

            return workerHourlyRate;
        }

        // PUT: api/WorkerHourlyRates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkerHourlyRate(Guid id, WorkerHourlyRate workerHourlyRate)
        {
            if (id != workerHourlyRate.Id)
            {
                return BadRequest();
            }

            _context.Entry(workerHourlyRate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkerHourlyRateExists(id))
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

        // POST: api/WorkerHourlyRates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WorkerHourlyRate>> PostWorkerHourlyRate(WorkerHourlyRate workerHourlyRate)
        {
            _context.WorkerHourlyRates.Add(workerHourlyRate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkerHourlyRate", new { id = workerHourlyRate.Id }, workerHourlyRate);
        }

        // DELETE: api/WorkerHourlyRates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkerHourlyRate(Guid id)
        {
            var workerHourlyRate = await _context.WorkerHourlyRates.FindAsync(id);
            if (workerHourlyRate == null)
            {
                return NotFound();
            }

            _context.WorkerHourlyRates.Remove(workerHourlyRate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkerHourlyRateExists(Guid id)
        {
            return _context.WorkerHourlyRates.Any(e => e.Id == id);
        }
    }
}
