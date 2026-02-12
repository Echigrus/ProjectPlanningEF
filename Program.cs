//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();

using Microsoft.EntityFrameworkCore;
using ProjectPlanningEF;
using ProjectPlanningEF.Models;
using System;

using (ApplicationContext db = new ApplicationContext())
{
    Worker[] workers = [ new Worker { Name = "Almaz" }, new Worker { Name = "Dmitry" } ];
    db.Workers.AddRange(workers);

    WorkerHourlyRate[] rates = [
        new WorkerHourlyRate { WorkerId = workers[0].Id, HourlyRate = 200, Start = new DateOnly(2026, 1, 1) },
        new WorkerHourlyRate { WorkerId = workers[0].Id, HourlyRate = 220, Start = new DateOnly(2025, 2, 1) },
        new WorkerHourlyRate { WorkerId = workers[1].Id, HourlyRate = 100, Start = new DateOnly(2026, 1, 1) },
    ];
    db.WorkerHourlyRates.AddRange(rates);

    Project project = new Project() { Name = "Test", Start = new DateOnly(2026, 1, 1), End = new DateOnly(2026, 12, 31) };
    db.Projects.Add(project);

    List<TimeTableDay> ttd = new List<TimeTableDay>();
    for (DateOnly i = project.Start; i <= project.End; i = i.AddDays(1))
    {
        byte h = i.DayOfWeek == DayOfWeek.Sunday || i.DayOfWeek == DayOfWeek.Saturday ? (byte)0 : (byte)8;
        ttd.Add(new TimeTableDay() { Date = i, WorkerId = workers[0].Id, Hours = h });
        ttd.Add(new TimeTableDay() { Date = i, WorkerId = workers[1].Id, Hours = h });
    }
    db.TimeTable.AddRange(ttd);

    db.SaveChanges();
}