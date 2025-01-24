public record UserUpdateRequest(
    Guid id,
    string Name,
    string Username,
    string Password,
    string Email,
    DateTime DateOfBirth
)
{
    public static User ConvertToEntity(UserUpdateRequest dto) => new User(
        dto.id,
        dto.Name,
        dto.Username,
        dto.Password,
        dto.Email,
        dto.DateOfBirth
    );
}