public interface IUserRepository
{
    public Task<List<User>> ReadAllUsers();
    public Task<User> ReadUsersById(Guid id);
    public Task<User> ReadUsersByUniqueAttributes(User user);
    public Task<User> CreateUser(User user);
    public Task<User> UpdateUser(User current, User updated);
    public Task<User> DeleteUser(User user);

}