using Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Service
{
    public class MMFdbContext : DbContext
    {
        public MMFdbContext() : base("MMFshopTable")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Furniture> Furnitures { get; set; }

        public virtual DbSet<Entry> Entrys { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<OrderFurniture> OrderFurnitures { get; set; }

    }
}
