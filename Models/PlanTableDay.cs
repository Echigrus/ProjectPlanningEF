using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectPlanningEF.Models
{
    /* Запись плана работ о дне и работе по назначению в нём */
    [PrimaryKey(nameof(Date), nameof(AssignmentId))]
    public class PlanTableDay
    {
        public DateOnly Date { get; set; }
        public Guid AssignmentId { get; set; }
        [ForeignKey("AssignmentId")]
        public Assignment? Assignment { get; set; }
        public float Hours { get; set; }
        public float Cost { get; set; }
    }
}
