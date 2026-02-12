using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectPlanningEF.Models
{
    /* Запись истории ставок сотрудника */
    public class WorkerHourlyRate
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid WorkerId { get; set; }
        [ForeignKey("WorkerId")]
        public Worker? Worker { get; set; }
        public float HourlyRate {  get; set; }
        public DateOnly Start { get; set; }
    }
}
