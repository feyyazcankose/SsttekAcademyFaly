namespace Faly.DataAccessLayer.Entities;

using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }

    // Navigation Properties
    public ICollection<Order> Orders { get; set; }
}
