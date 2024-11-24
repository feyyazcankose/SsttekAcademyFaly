namespace Faly.DataAccessLayer.Entities;

public class OrderDetail
{
    public int OrderId { get; set; }
    public int CourseId { get; set; }
    public decimal Price { get; set; } // Sipariş sırasında kursun fiyatı
    public int Quantity { get; set; } // Örneğin, kurs sayısı (genelde 1)

    // Navigation Properties
    public Order Order { get; set; }
    public Course Course { get; set; }
}
