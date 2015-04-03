using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timesheets.Data
{
    public class TimesheetDbConfiguration : DbConfiguration
    {
        public TimesheetDbConfiguration()
        {
            SetDefaultConnectionFactory(new LocalDbConnectionFactory("v11.0"));

            SetDatabaseInitializer(new DropCreateDatabaseIfModelChanges<TimesheetDbContext>());

            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
        }
    }
}
