using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectPlanningEF.Models
{
    /* Запись из внешнего табеля рабочего времени */
    [PrimaryKey(nameof(Date), nameof(WorkerId))]
    public class TimeTableDay
    {
        public DateOnly Date { get; set; }
        public Guid WorkerId { get; set; }
        [ForeignKey("WorkerId")]
        public Worker? Worker { get; set; }
        public byte Hours { get; set; }
    }
}
