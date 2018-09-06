using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public string OrderName { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int CustomerID { get; set; }

        [ForeignKey("OrderId")]
        public virtual List<Entry> Entrys { get; set; }

        [ForeignKey("OrderId")]
        public virtual List<OrderFurniture> OrderTeches { get; set; }
    }
}
