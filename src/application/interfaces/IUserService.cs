public interface IUserService
{
    public List<UserReadDto> ReadAllUsers();
    public UserReadDto ReadUsersById(Guid id);
    public UserCreateDto CreateUser(UserCreateDto dto);
    public UserCreateDto UpdateUser(Guid id, UserCreateDto dto);
    public UserReadDto DeleteUser(Guid id);
}