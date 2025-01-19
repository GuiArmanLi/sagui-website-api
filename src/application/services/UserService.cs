public class UserService : IUserService
{
    private static List<User> _users = new List<User>();

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

    public UserReadDto ReadUsersById(Guid id)
    {
        return _users.FirstOrDefault(user => user.Id == id) ?? throw new ArgumentNullException($"User with {id} not found");
    }

    public UserCreateDto CreateUser(UserCreateDto dto)
    {
        if (dto == null)
            throw new ArgumentNullException("User is null");

        _users.Add(dto);
        return dto;
    }

    public UserCreateDto UpdateUser(Guid id, UserCreateDto dto)
    {
        if (dto == null)
            throw new ArgumentNullException("User is null");

        var updatedUser = _users.FirstOrDefault(user => user.Id == id)
         ?? throw new ArgumentNullException($"There is not any user with id {id}");

        //Utils.UpdateObject(currentUser, updatedUser);

        return dto;
    }

    public UserReadDto DeleteUser(Guid id)
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