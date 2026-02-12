using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectPlanningEF.Models
{
    /* Назначение сотрудника на проект */
    public class Assignment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public Project? Project { get; set; }
        public Guid WorkerId { get; set; }
        [ForeignKey("WorkerId")]
        public Worker? Worker { get; set; }
        public byte LoadShare { get; set; }
        public DateOnly Start {  get; set; }
        public DateOnly End { get; set; }
    }
}
