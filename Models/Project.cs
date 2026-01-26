namespace ProjectPlanningEF.Models
{
    public class Project
    {
        public Guid Id { get; set; }
        public DateOnly Start { get; set; }
        public DateOnly End { get; set; }
    }
}
