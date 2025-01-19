using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("users/")]
public class UserController
{
    private IUserService _service;
    public UserController(IUserService service) => _service = service;

    [HttpGet]
    public Task<IActionResult> ReadAllUsers()
    {
        return _service.ReadAllUsers();
    }

    [HttpGet("/{id:Guid}")]
    public Task<IActionResult> ReadUsersById([FromRoute] Guid id)
    {
        return _service.ReadUsersById(id);
    }

    [HttpPost]
    public Task<IActionResult> CreateUser([FromBody] UserCreateDto dto)
    {
        return _service.CreateUser(dto);
    }

    [HttpPut("{id:Guid}")]
    public Task<IActionResult> UpdateUser([FromRoute] Guid id, [FromBody] UserUpdateDto dto)
    {
        return _service.UpdateUser(id, dto);
    }

    [HttpDelete("{id:Guid}")]
    public Task<IActionResult> DeleteUser([FromRoute] Guid id)
    {
        return _service.DeleteUser(id);
    }

}