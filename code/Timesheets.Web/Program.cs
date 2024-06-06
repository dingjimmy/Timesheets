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

app.MapGet("/timesheets", () => "This is a list of timesheets");            // list timesheets
app.MapPost("/timesheet", () => "New timesheet added!");                    // add new timesheet
app.MapPut("/timesheet/{id}", (string id) => $"Updating timesheet {id}");   // update timesheet {id}
app.MapGet("/timesheet/{id}", (string id) => $"Viewing timesheet {id}");    // view timesheet {id}

app.Run();