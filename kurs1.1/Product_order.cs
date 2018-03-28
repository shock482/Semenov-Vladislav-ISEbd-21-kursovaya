namespace kurs1._1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product-order")]
    public partial class Product_order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderID { get; set; }

        [Column("Order code")]
        public int Order_code { get; set; }

        [Column("Product code")]
        public int Product_code { get; set; }

        public int Quantity { get; set; }

        public int Value { get; set; }
    }
}
