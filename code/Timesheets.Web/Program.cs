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

app.MapGet("/timesheets", () => MockResponseHandlers.ListTimesheets());                                                        // list timesheets
app.MapPost("/timesheet", (AddTimesheetRequest request) => MockResponseHandlers.AddTimesheet(request));                        // add new timesheet
app.MapPut("/timesheet/{id}", (int id, UpdateTimesheetRequest request) => MockResponseHandlers.UpdateTimesheet(id, request));  // update timesheet {id}
app.MapGet("/timesheet/{id}", (int id) => MockResponseHandlers.ViewTimesheet(id))
   .WithName("view-timesheet");                                                                                                // view timesheet {id}

app.Run();



public class MockResponseHandlers
{
    private static readonly TimesheetValidator _Validator = new TimesheetValidator();

    static readonly List<Timesheet> _Timesheets = [
        new Timesheet(1001,"2024 Week 5","HM Government","Winston Churchill",DateTime.Now.ToString() ,DateTime.Now.AddDays(1).ToString()),
        new Timesheet(1002,"2024 Week 6","HM Government","Winston Churchill",DateTime.Now.ToString(), DateTime.Now.AddDays(1).ToString()),
        new Timesheet(1003,"2024 Week 7","HM Government","Winston Churchill",DateTime.Now.ToString(), DateTime.Now.AddDays(1).ToString())
    ];

    public static IResult ListTimesheets()
    {
        return Results.Ok(_Timesheets);
    }

    public static IResult ViewTimesheet(int id)
    {
        if (!_Timesheets.Any(s => s.ID == id))
        {
            return Results.NotFound();
        }

        return Results.Ok(_Timesheets.First(ts => ts.ID == id));
    }
   
    public static IResult AddTimesheet(AddTimesheetRequest request)
    {
        if (!_Validator.IsValid(request.Name, request.Customer, request.Employee, request.PeriodStarts, request.PeriodEnds))
        {
            return Results.BadRequest();
        }

        try
        {
            var nextId = _Timesheets.Last().ID + 1;
            var timesheet = new Timesheet(nextId, request.Name, request.Customer, request.Employee, request.PeriodStarts, request.PeriodEnds);
            _Timesheets.Add(timesheet);

            return Results.CreatedAtRoute("view-timesheet", new { timesheet.ID });
        }
        catch
        {
            //TODO: log error
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    public static IResult UpdateTimesheet(int id, UpdateTimesheetRequest request)
    {
        if (!_Validator.IsValid(request.Name, request.Customer, request.Employee, request.PeriodStarts, request.PeriodEnds))
        {
            return Results.BadRequest();
        }
        
        if (!_Timesheets.Any(s => s.ID == id))
        {
            return Results.NotFound();
        }

        try
        {
            var timesheet = new Timesheet(id, request.Name, request.Customer, request.Employee, request.PeriodStarts, request.PeriodEnds);
            var idx = _Timesheets.FindIndex(ts => ts.ID == id);
            _Timesheets[idx] = timesheet;

            return Results.Ok();
        }
        catch     
        {
            //TODO: log error
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}


public class Timesheet
{
    public int ID { get; set; }

    public string Name { get; set; }
    
    public string Employee { get; set; }
    
    public string Customer { get; set; }
    
    public DateTime PeriodEnds { get; set; }
    
    public DateTime PeriodStarts { get; set; }

    public Timesheet(int id, string? name, string? customer, string? employee, string? periodEnds, string? periodStarts)
    {
        ID = id;
        Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));
        Customer = !string.IsNullOrEmpty(customer) ? customer : throw new ArgumentException($"'{nameof(customer)}' cannot be null or empty.", nameof(customer)); ;
        Employee = !string.IsNullOrEmpty(employee) ? employee : throw new ArgumentException($"'{nameof(employee)}' cannot be null or empty.", nameof(employee));

        DateTime pStarts;
        PeriodStarts = DateTime.TryParse(periodStarts, out pStarts) ? pStarts : throw new ArgumentException($"'{nameof(periodStarts)} does not represent a valid date or time.");

        DateTime pEnds;
        PeriodEnds = DateTime.TryParse(periodEnds, out pEnds) ? pEnds : throw new ArgumentException($"'{nameof(periodEnds)} does not represent a valid date or time.");
    }
}

public class TimesheetValidator()
{
    public bool IsValid(string? name, string? customer, string? employee, string? periodStarts, string? periodEnds)
    {
        if (string.IsNullOrWhiteSpace(name)) return false;
        if (string.IsNullOrWhiteSpace(customer)) return false;
        if (string.IsNullOrWhiteSpace(employee)) return false;
        if (!DateTime.TryParse(periodStarts, out _)) return false;
        if (!DateTime.TryParse(periodEnds, out _)) return false;
        
        return true;
    }
}

public class AddTimesheetRequest
{
    public string? Name { get; set; }
    
    public string? Employee { get; set; }
    
    public string? Customer { get; set; }
    
    public string? PeriodEnds { get; set; }
    
    public string? PeriodStarts { get; set; }
}

public class UpdateTimesheetRequest
{
    public string? Name { get; set; }
    
    public string? Employee { get; set; }
    
    public string? Customer { get; set; }
    
    public string? PeriodEnds { get; set; }
    
    public string? PeriodStarts { get; set; }
}