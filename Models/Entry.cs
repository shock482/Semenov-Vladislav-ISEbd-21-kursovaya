using System;

namespace Models
{
    public class Entry
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int OrderId { get; set; }

        public decimal Sum { get; set; }

        public decimal SumPay { get; set; }

        public PaymentState Status { get; set; }

        public DateTime DateCreate { get; set; }

        public string DateVisit { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Order Order { get; set; }

    }
}
