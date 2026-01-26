namespace ProjectPlanningEF.Models
{
    public class TimeTableDay
    {
        public DateOnly Date { get; set; }
        public Guid WorkerId { get; set; }
        public byte Hours { get; set; }
    }
}
