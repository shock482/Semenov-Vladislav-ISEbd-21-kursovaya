namespace kurs1._1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductID { get; set; }

        [Column("Name of product")]
        [Required]
        [StringLength(50)]
        public string Name_of_product { get; set; }

        [Required]
        [StringLength(50)]
        public string Price { get; set; }

        [Column("Product description")]
        [Required]
        [StringLength(50)]
        public string Product_description { get; set; }
    }
}
