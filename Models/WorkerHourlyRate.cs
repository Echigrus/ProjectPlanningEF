namespace ProjectPlanningEF.Models
{
    public class WorkerHourlyRate
    {
        public Guid Id { get; set; }
        public Guid WorkerId { get; set; }
        public float HourlyRate {  get; set; }
        public DateOnly Start { get; set; }
    }
}
