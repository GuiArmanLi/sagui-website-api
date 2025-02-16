using Microsoft.AspNetCore.Identity;

public interface IIdentityService
{
    Task<IEnumerable<UserGeneralResponse>> ReadAllUsersAsync();
    Task<object> ReadWholeUserByIdAsync(Guid id);
    Task<UserGeneralResponse> ReadUserByIdAsync(Guid id);
    Task<UserGeneralResponse> ReadUserByEmailAsync(string email);

    Task<UserSignUpResponse> SignUpUserAsync(UserSignUpRequest request);
    Task<UserSignInResponse> SignInUserAsync(UserSignInRequest request);
    Task<UserSignInResponse> RefreshSignInUserAsync(Guid id);

    Task<UserGeneralResponse> ChangeEmailAsync(Guid id, string newEmail);
    Task<UserGeneralResponse> ChangeUsernameAsync(Guid id, string newUsername);
    Task<UserGeneralResponse> ChangePhoneAsync(Guid id, string newPhoneNumber);
    Task<UserGeneralResponse> ChangePasswordAsync(Guid id, ChangePasswordRequest request);
    Task<UserGeneralResponse> ChangeActiveUserByIdAsync(Guid id);

    Task<UserGeneralResponse> DeleteUserByIdAsync(Guid id);
}