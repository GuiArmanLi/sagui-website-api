public record UserUpdateDto(
    string Name,
    string Username,
    string Password,
    string Email
)
{
    public static implicit operator User(UserUpdateDto dto) => new User(
        dto.Name,
        dto.Username,
        dto.Password,
        dto.Email,
        DateTime.Now
    );
}