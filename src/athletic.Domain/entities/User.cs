public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; }

    public User(string name,
        string username,
        string password,
        string email,
        DateTime dateOfBirth)
    {
        Id = Guid.NewGuid();
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