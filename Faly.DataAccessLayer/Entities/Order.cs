namespace Faly.DataAccessLayer.Entities;

public class Order
{
    public int Id { get; set; }
    public string UserId { get; set; } // Identity'nin UserId'si
    public DateTime OrderDate { get; set; }
    public decimal TotalPrice { get; set; } // Siparişin toplam tutarı

    // Navigation Properties
    public ApplicationUser User { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; }
}
