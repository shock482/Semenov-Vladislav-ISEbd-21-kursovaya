namespace Service.ViewModel
{
    public class EntryViewModel
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public string CustomerFIO { get; set; }

        public int OrderId { get; set; }

        public string OrderName { get; set; }

        public decimal Sum { get; set; }

        public decimal SumPay { get; set; }

        public string Status { get; set; }

        public string DateCreate { get; set; }

        public string DateVisit { get; set; }
    }
}
