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
    public IActionResult CreateUser([FromBody] UserCreateDto dto)
    {
        return _service.CreateUser(dto);
    }

    [HttpPut("{id:Guid}")]
    public IActionResult UpdateUser([FromRoute] Guid id, [FromBody] UserUpdateDto dto)
    {
        return _service.UpdateUser(id, dto);
    }

    [HttpDelete("{id:Guid}")]
    public IActionResult DeleteUser([FromRoute] Guid id)
    {
        return _service.DeleteUser(id);
    }

}