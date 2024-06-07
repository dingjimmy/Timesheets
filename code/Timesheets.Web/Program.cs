using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Timesheets.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TimesheetDbContext>(options =>
{
    options.UseInMemoryDatabase("timesheets");
});

var app = builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/timesheets", () => MockResponseHandlers.ListTimesheets());                                 // list timesheets
app.MapPost("/timesheet", (Timesheet ts) => MockResponseHandlers.AddTimesheet(ts));                     // add new timesheet
app.MapPut("/timesheet/{id}", (int id, Timesheet ts) => MockResponseHandlers.UpdateTimesheet(id, ts));  // update timesheet {id}
app.MapGet("/timesheet/{id}", (int id) => MockResponseHandlers.ViewTimesheet(id))
   .WithName("view-timesheet");                                                                         // view timesheet {id}

app.Run();



public class MockResponseHandlers
{
    static readonly List<Timesheet> _Timesheets = [
        new Timesheet(1001,"2024 Week 5","HM Government","Winston Churchill",DateTime.Now,DateTime.Now.AddDays(1)),
        new Timesheet(1002,"2024 Week 6","HM Government","Winston Churchill",DateTime.Now,DateTime.Now.AddDays(1)),
        new Timesheet(1003,"2024 Week 7","HM Government","Winston Churchill",DateTime.Now,DateTime.Now.AddDays(1))
    ];

    public static IResult ListTimesheets()
    {
        return Results.Ok(_Timesheets);
    }

    public static IResult AddTimesheet(Timesheet ts)
    {
        if (ts.ID == 0)
        {
            return Results.BadRequest();
        }

        if (_Timesheets.Any(s => s.ID == ts.ID))
        {
            return Results.BadRequest();
        }

        ts.ID = _Timesheets.Last().ID++;

        _Timesheets.Add(ts);

        return Results.CreatedAtRoute("view-timesheet", { ID = ts.ID});
    }

    public static IResult UpdateTimesheet(int id, Timesheet ts)
    {
        if (!_Timesheets.Any(s => s.ID == id))
        {
            return Results.NotFound();
        }
        
        _Timesheets[id] = ts;

        return Results.Ok();
    }

    public static IResult ViewTimesheet(int id)
    {
        if (!_Timesheets.Any(s => s.ID == id))
        {
            return Results.NotFound();
        }

        return Results.Ok(_Timesheets.First(ts => ts.ID == id));
    }
}


public class Timesheet
{
    public int ID { get; internal set; }
    public DateTime PeriodEnds { get; internal set; }
    public DateTime PeriodStarts { get; internal set; }
    public string Employee { get; internal set; }
    public string Customer { get; internal set; }
    public string Name { get; internal set; }

    public Timesheet(int id, string name, string customer, string employee, DateTime periodEnds, DateTime periodStarts)
    {
        Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));
        Customer = !string.IsNullOrEmpty(customer) ? customer : throw new ArgumentException($"'{nameof(customer)}' cannot be null or empty.", nameof(customer)); ;
        Employee = !string.IsNullOrEmpty(employee) ? employee : throw new ArgumentException($"'{nameof(employee)}' cannot be null or empty.", nameof(employee));
        ID = id;
        PeriodEnds = periodEnds;
        PeriodStarts = periodStarts;
    }
}