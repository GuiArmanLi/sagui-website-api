public interface IUserRepository
{
    public Task<List<User>> ReadAllUsers();
    public Task<User> ReadUserById(Guid id);
    public Task<User> ReadUserByEmail(string email);
    public Task<User> CreateUser(User user);
    public Task<User> UpdateUser(User current, User updated);
    public Task<User> DeleteUser(User user);

}