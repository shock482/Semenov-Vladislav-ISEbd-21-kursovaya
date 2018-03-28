namespace kurs1._1
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Balance> Balance { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Product_order> Product_order { get; set; }
        public virtual DbSet<Reservation_Order_> Reservation_Order_ { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation_Order_>()
                .Property(e => e.Order_code)
                .IsFixedLength();

            modelBuilder.Entity<Reservation_Order_>()
                .Property(e => e.Order_date)
                .IsFixedLength();
        }
    }
}
