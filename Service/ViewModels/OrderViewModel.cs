using System.Collections.Generic;

namespace Service.ViewModel
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public string OrderName { get; set; }

        public decimal Price { get; set; }
        public int CustomerID { get; set; }
        public List<OrderFurnitureViewModel> OrderFurnitures { get; set; }
    }
}
