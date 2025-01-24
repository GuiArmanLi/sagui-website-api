public class User : Entity
{
    public string Name { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; }

    public User(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public User(
        Guid id,
        string name,
        string username,
        string password,
        string email,
        DateTime dateOfBirth)
    {
        Id = id;
        Name = name;
        Username = username;
        Password = password;
        Email = email;
        DateOfBirth = dateOfBirth;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        IsActive = true;
    }
}