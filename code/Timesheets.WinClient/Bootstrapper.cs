using Caliburn.Micro;
using Ninject;
using System.Windows;
using Timesheets.Data;
using System;
using System.Collections.Generic;

namespace Timesheets.WinClient
{
    public class Bootstrapper : BootstrapperBase
    {
        private IKernel kernel;

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            this.kernel = new StandardKernel();

            this.kernel.Bind<IWindowManager>().To<WindowManager>().InSingletonScope();
            this.kernel.Bind<IEventAggregator>().To<EventAggregator>().InSingletonScope();
            this.kernel.Bind<ITimesheetDbContext>().To<TimesheetDbContext>();

            base.Configure();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<RootViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return kernel.Get(service);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return kernel.GetAll(service);
        }
    }
}
