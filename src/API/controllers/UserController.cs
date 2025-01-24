using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("users/")]
public class UserController
{
    private IUserService _service;
    public UserController(IUserService service) => _service = service;

    [HttpGet]
    public IActionResult ReadAllUsers()
    {
        return _service.ReadAllUsers();
    }

    [HttpGet("/{id:Guid}")]
    public IActionResult ReadUsersById([FromRoute] Guid id)
    {
        return _service.ReadUsersById(id);
    }

    [HttpPost]
    public IActionResult CreateUser([FromBody] UserCreateRequest request)
    {
        return _service.CreateUser(request);
    }

    public IActionResult LoginUser([FromBody] UserLoginRequest request){
        return _service.LoginUser(request);
    }

    [HttpPut("{id:Guid}")]
    public IActionResult UpdateUser([FromRoute] Guid id, [FromBody] UserUpdateRequest request)
    {
        return _service.UpdateUser(id, request);
    }

    [HttpDelete("{id:Guid}")]
    public IActionResult DeleteUser([FromRoute] Guid id)
    {
        return _service.DeleteUser(id);
    }

}