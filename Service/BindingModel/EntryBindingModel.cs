namespace Service.BindingModel
{
    public class EntryBindingModel
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int OrderId { get; set; }

        public decimal Sum { get; set; }

        public decimal SumPay { get; set; }

        public string DataVisit { get; set; }

    }
}
