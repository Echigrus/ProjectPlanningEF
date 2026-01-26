namespace ProjectPlanningEF.Models
{
    public class PlanTableDay
    {
        public DateOnly date { get; set; }
        public Guid AssignmentId { get; set; }
        public float Hours { get; set; }
        public float Cost { get; set; }
    }
}
