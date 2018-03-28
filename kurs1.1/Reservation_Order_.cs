namespace kurs1._1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Reservation(Order)")]
    public partial class Reservation_Order_
    {
        [Key]
        [Column("Order code")]
        [StringLength(10)]
        public string Order_code { get; set; }

        [Column("Buyer code")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Buyer_code { get; set; }

        [Column("Order date")]
        [StringLength(10)]
        public string Order_date { get; set; }

        [Column("Order status")]
        public int? Order_status { get; set; }
    }
}
