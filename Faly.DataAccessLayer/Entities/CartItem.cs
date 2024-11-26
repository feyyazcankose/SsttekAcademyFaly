using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faly.DataAccessLayer.Entities;

public class CartItem
{
    [Key]
    public int Id { get; set; }

    public int CourseId { get; set; }

    public string CourseName { get; set; } = default!;

    public decimal Price { get; set; }

    public int Quantity { get; set; } = 1; // Miktar ekleniyor

    [ForeignKey("Cart")]
    public int CartId { get; set; }

    public Cart Cart { get; set; } = default!;
}
