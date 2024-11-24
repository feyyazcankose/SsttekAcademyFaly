namespace Faly.DataAccessLayer.Entities;

public class Payment
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public decimal Amount { get; set; } // Ödeme tutarı
    public string PaymentStatus { get; set; } // "Success", "Failed" vb.
    public DateTime PaymentDate { get; set; }

    // Navigation Properties
    public Order Order { get; set; }
}
