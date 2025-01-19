using Microsoft.AspNetCore.Mvc;

public interface IUserService
{
    public Task<IActionResult> ReadAllUsers();
    public Task<IActionResult> ReadUsersById(Guid id);
    public Task<IActionResult> CreateUser(UserCreateDto dto);
    public Task<IActionResult> UpdateUser(Guid id, UserUpdateDto dto);
    public Task<IActionResult> DeleteUser(Guid id);
}