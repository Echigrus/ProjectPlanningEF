using Microsoft.EntityFrameworkCore;
using ProjectPlanningEF.Models;

namespace ProjectPlanningEF
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Worker> Workers => Set<Worker>();
        public DbSet<WorkerHourlyRate> WorkerHourlyRates => Set<WorkerHourlyRate>();
        public DbSet<Project> Projects => Set<Project>();
        public DbSet<Assignment> Assignments => Set<Assignment>();
        public DbSet<TimeTableDay> TimeTable => Set<TimeTableDay>();
        public DbSet<PlanTableDay> PlanTable => Set<PlanTableDay>();
        public ApplicationContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=projectplanning.db");
        }
    }
}
