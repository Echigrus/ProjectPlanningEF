using System.ComponentModel.DataAnnotations;

namespace ProjectPlanningEF.Models
{
    /* Сотрудник, упрощённое представление */
    public class Worker
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string? Name { get; set; }
    }
}
