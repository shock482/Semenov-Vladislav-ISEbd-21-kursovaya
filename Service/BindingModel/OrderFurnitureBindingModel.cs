namespace Service.BindingModel
{
    public class OrderFurnitureBindingModel
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int FurnitureId { get; set; }

        public decimal Price { get; set; }
    }
}
