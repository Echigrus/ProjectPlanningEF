namespace ProjectPlanningEF.Models
{
    public class Assignment
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Guid WorkerId { get; set; }
        public byte LoadShare { get; set; }
        public DateOnly Start {  get; set; }
        public DateTime End { get; set; }
    }
}
