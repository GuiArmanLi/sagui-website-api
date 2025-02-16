using Microsoft.AspNetCore.Identity;

public class User : IdentityUser
{
    public bool Active { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}