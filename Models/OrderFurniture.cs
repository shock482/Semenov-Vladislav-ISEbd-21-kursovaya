namespace Models
{
    public class OrderFurniture
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int FurnitureId { get; set; }

        public int Count { get; set; }

        public decimal Price { get; set; }

        public virtual Order Order { get; set; }

        public virtual Furniture Furniture { get; set; }
    }
}
