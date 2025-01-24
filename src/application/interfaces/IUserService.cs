using Microsoft.AspNetCore.Mvc;

public interface IUserService
{
    public IActionResult ReadAllUsers();
    public IActionResult ReadUsersById(Guid id);
    public IActionResult CreateUser(UserCreateRequest request);
    public IActionResult LoginUser(UserLoginRequest request);
    public IActionResult UpdateUser(Guid id, UserUpdateRequest request);
    public IActionResult DeleteUser(Guid id);
}