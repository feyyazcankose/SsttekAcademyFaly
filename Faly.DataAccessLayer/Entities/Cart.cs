using System.ComponentModel.DataAnnotations;

namespace Faly.DataAccessLayer.Entities;

public class Cart
{
    [Key]
    public int Id { get; set; }

    public string UserId { get; set; } = default!;

    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}
