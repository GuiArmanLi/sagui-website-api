public record UserResponse(
    string Name,
    string Username,
    string Email,
    DateTime DateOfBirth
)
{
    public static UserResponse ConvertToUserResponse(User user)
    {
        return new UserResponse(
            user.Name,
            user.Username,
            user.Email,
            user.DateOfBirth
        );
    }
}