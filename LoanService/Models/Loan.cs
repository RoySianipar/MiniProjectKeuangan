namespace LoanService.Models
{
    public class Loan
    {
        public int LoanId { get; set; }
        public int CustomerId { get; set; }  // Foreign Key ke Customer
        public decimal Amount { get; set; }
        public int DurationMonths { get; set; }
        public decimal InterestRate { get; set; }
        public string Status { get; set; } = "Pending"; 
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
