public class ChangePasswordRequest
{
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }

    public ChangePasswordRequest(string currentPassword, string newPassword)
    {
        CurrentPassword = currentPassword ?? throw new ArgumentNullException(nameof(currentPassword));
        NewPassword = newPassword ?? throw new ArgumentNullException(nameof(newPassword));
    }
}