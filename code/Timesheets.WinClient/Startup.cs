using Caliburn.Micro;
using Ninject;
using System.Windows;
using Timesheets.Data;
using System;
using System.Collections.Generic;

namespace Timesheets.WinClient
{
    public class Startup : BootstrapperBase
    {
        private IKernel ninject;

        public Startup()
        {
            Initialize();
        }

        protected override void Configure()
        {
            this.ninject = new StandardKernel();

            this.ninject.Bind<IWindowManager>().To<WindowManager>().InSingletonScope();
            this.ninject.Bind<IEventAggregator>().To<EventAggregator>().InSingletonScope();
            //this.ninject.Bind<ITimesheetDbContext>().To<TimesheetDbContext>();
            this.ninject.Bind<ITimesheetDbContext>().To<FakeTimehsheetDbContext>();
            
            base.Configure();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            var settings = new Dictionary<string, object>
            {
                { "SizeToContent", SizeToContent.Manual },
                { "Height" , 600  },
                { "Width"  , 800 },
            };

            DisplayRootViewFor<ShellViewModel>(settings);
        }

        protected override object GetInstance(Type service, string key)
        {
            return ninject.Get(service);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return ninject.GetAll(service);
        }
    }
}
