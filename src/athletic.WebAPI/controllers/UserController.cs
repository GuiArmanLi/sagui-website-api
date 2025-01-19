using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("users/")]
public class UserController
{
    private static List<User> _users = new List<User>();

    [HttpGet]
    public List<UserReadDto> ReadAllUsers()
    {
        return _users.Select(user => new UserReadDto
        (
             user.Id,
             user.Name,
             user.Username,
             user.Password,
             user.Email,
             user.DateOfBirth
        )).ToList();
    }

    [HttpGet("/{id:Guid}")]
    public UserReadDto ReadUsersById([FromRoute] Guid id)
    {
        return _users.FirstOrDefault(user => user.Id == id) ?? throw new ArgumentNullException($"User with {id} not found");
    }

    [HttpPost]
    public UserCreateDto CreateUser([FromBody] UserCreateDto dto)
    {
        if (dto == null)
            throw new ArgumentNullException("User is null");

        _users.Add(dto);
        return dto;
    }

    [HttpPut("{id:Guid}")]
    public UserCreateDto UpdateUser([FromRoute] Guid id, [FromBody] UserCreateDto dto)
    {
        if (dto == null)
            throw new ArgumentNullException("User is null");

        var updatedUser = _users.FirstOrDefault(user => user.Id == id)
         ?? throw new ArgumentNullException($"There is not any user with id {id}");

        //Utils.UpdateObject(currentUser, updatedUser);

        return dto;
    }

    [HttpDelete("{id:Guid}")]
    public UserReadDto DeleteUser([FromRoute] Guid id)
    {
        var user = _users.Find(user => user.Id == id);
        if (user != null)
        {
            _users.Remove(user);

            //Utils.UpdateObject();

            UserReadDto response = user;
            return response;
        }
        throw new ArgumentNullException($"There os not any user with id {id}");
    }

}