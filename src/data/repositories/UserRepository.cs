
using Microsoft.EntityFrameworkCore;

public class UserRepository(DataContext context) : IUserRepository
{
    private DataContext _context = context;

    public async Task<List<User>> ReadAllUsers()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User> ReadUserById(Guid id)
    {
        return (await _context.Users.FirstOrDefaultAsync(user => user.Id == id))!;
    }

    public async Task<User> ReadUserByEmail(string email)
    {
        return (await _context.Users.FirstOrDefaultAsync(user => user.Email == email))!;
    }

    public async Task<User> CreateUser(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<User> UpdateUser(User current, User updated)
    {
        //Utils.UpdateObject(currentUser, updatedUser);
        await _context.SaveChangesAsync();

        return updated;
    }

    public async Task<User> DeleteUser(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return user;
    }

}