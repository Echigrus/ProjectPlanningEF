using System.ComponentModel.DataAnnotations;

namespace ProjectPlanningEF.Models
{
    /* Проект */
    public class Project
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string? Name { get; set; }
        public DateOnly Start { get; set; }
        public DateOnly End { get; set; }
    }
}
