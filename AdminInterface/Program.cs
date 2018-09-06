using Service;
using Service.ImplementationsList;
using Service.Interfaces;
using System;
using System.Data.Entity;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;

namespace AdminInterface
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormMain>());
        }

        public static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<DbContext, MMFdbContext>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICustomerService, CustomerService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IFurnitureService, FurnitureService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IOrderService, OrderService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMainService, MainService>(new HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}
