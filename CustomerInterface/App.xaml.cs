using Service;
using Service.ImplementationsList;
using Service.Interfaces;
using System;
using System.Data.Entity;
using System.Windows;
using Unity;
using Unity.Lifetime;

namespace CustomerInterface
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        [STAThread]
        public static void Main()
        {
            var container = BuildUnityContainer();

            var application = new App();
            application.Run(container.Resolve<FormBasic>());

        }

        public static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<DbContext, MMFdbContext>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICustomerService, CustomerService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IFurnitureService, FurnitureService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IOrderService, OrderService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMainService, MainService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IReportService, ReportService>(new HierarchicalLifetimeManager());

            return currentContainer;
        }
    }
}
