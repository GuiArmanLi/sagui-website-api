public record UserCreateDto(
    string Name,
    string Username,
    string Password,
    string Email,
    DateTime DateOfBirth
)
{
    public static implicit operator User(UserCreateDto dto) => new User(
        dto.Name,
        dto.Username,
        dto.Password,
        dto.Email,
        dto.DateOfBirth
    );
}