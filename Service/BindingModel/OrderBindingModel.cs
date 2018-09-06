using System.Collections.Generic;

namespace Service.BindingModel
{
    public class OrderBindingModel
    {
        public int Id { get; set; }

        public string OrderName { get; set; }

        public decimal Price { get; set; }
        public int CustomerID { get; set; }

        public List<OrderFurnitureBindingModel> OrderFurnitures { get; set; }
    }
}
