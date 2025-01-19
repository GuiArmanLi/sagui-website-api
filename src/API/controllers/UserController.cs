using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("users/")]
public class UserController
{
    private IUserService _service;
    public UserController(IUserService service) => _service = service;

    [HttpGet]
    public List<UserReadDto> ReadAllUsers()
    {
        return _service.ReadAllUsers();
    }

    [HttpGet("/{id:Guid}")]
    public UserReadDto ReadUsersById([FromRoute] Guid id)
    {
        return _service.ReadUsersById(id);
    }

    [HttpPost]
    public UserCreateDto CreateUser([FromBody] UserCreateDto dto)
    {
        return _service.CreateUser(dto);
    }

    [HttpPut("{id:Guid}")]
    public UserCreateDto UpdateUser([FromRoute] Guid id, [FromBody] UserCreateDto dto)
    {
        return _service.UpdateUser(id, dto);
    }

    [HttpDelete("{id:Guid}")]
    public UserReadDto DeleteUser([FromRoute] Guid id)
    {
        return _service.DeleteUser(id);
    }

}