using Microsoft.AspNetCore.Mvc;

public interface IUserService
{
    public IActionResult ReadAllUsers();
    public IActionResult ReadUsersById(Guid id);
    public IActionResult CreateUser(UserCreateDto dto);
    public IActionResult UpdateUser(Guid id, UserUpdateDto dto);
    public IActionResult DeleteUser(Guid id);
}