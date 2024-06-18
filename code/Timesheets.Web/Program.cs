using Microsoft.EntityFrameworkCore;
using Timesheets.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc();
builder.Services.AddDbContext<TimesheetDbContext>(options =>
{
    options.UseInMemoryDatabase("timesheets");
});

var app = builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles();
app.MapDefaultControllerRoute();

app.Run();

namespace Timesheets.Domain
{
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

}

namespace Timesheets.Web.Models
{ 

    public class AddTimesheetViewModel
    {
        public string? Name { get; set; }

        public string? Employee { get; set; }

        public string? Customer { get; set; }

        public string? PeriodEnds { get; set; }

        public string? PeriodStarts { get; set; }
    }

    public class UpdateTimesheetViewModel
    {
        public string? Name { get; set; }

        public string? Employee { get; set; }

        public string? Customer { get; set; }

        public string? PeriodEnds { get; set; }

        public string? PeriodStarts { get; set; }
    }

}