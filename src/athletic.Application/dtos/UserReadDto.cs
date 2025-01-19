public record UserReadDto(
    Guid Id,
    string Name,
    string Username,
    string Password,
    string Email,
    DateTime DateOfBirth
)
{
    public static implicit operator UserReadDto(User user)
    {
        return new UserReadDto(
        user.Id,
        user.Name,
        user.Username,
        user.Password,
        user.Email,
        user.DateOfBirth
        );
    }
}