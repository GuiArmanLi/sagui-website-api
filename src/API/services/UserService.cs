using Microsoft.AspNetCore.Mvc;
using System.Net;

using Microsoft.EntityFrameworkCore;

public class UserService : IUserService
{
    private AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> ReadAllUsers()
    {
        var users = await _context.Users.Select(user => (UserReadDto)user).ToListAsync();

        return new CustomResponse(HttpStatusCode.Accepted, users);
    }

    public async Task<IActionResult> ReadUsersById(Guid id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
        if (user == null)
            return new CustomResponse(HttpStatusCode.BadRequest, $"User with {id} not found");

        return new CustomResponse(HttpStatusCode.Accepted, (UserReadDto)user);
    }

    public async Task<IActionResult> CreateUser(UserCreateDto request)
    {
        if (request == null)
            return new CustomResponse(HttpStatusCode.BadRequest, "Request is null");

        var user = await _context.Users.FirstOrDefaultAsync(
            user => user.Username == request.Username
            || user.Email == request.Email);

        if (user != null)
            return new CustomResponse(
                HttpStatusCode.BadRequest,
                $"Already exist an user with Username {request.Username} or Email {request.Email}"
            );

        var userRequest = (User)request;

        await _context.AddAsync(userRequest);
        await _context.SaveChangesAsync();

        return new CustomResponse(HttpStatusCode.Created, request); //Retornar DTO
    }

    public async Task<IActionResult> UpdateUser(Guid id, UserUpdateDto request)
    {
        if (request == null)
            return new CustomResponse(HttpStatusCode.BadRequest, "Request is null");

        var currentUser = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
        if (currentUser == null)
            return new CustomResponse(HttpStatusCode.BadRequest, $"User with {id} not found");

        //Utils.UpdateObject(currentUser, updatedUser);

        return new CustomResponse(HttpStatusCode.Accepted, (UserReadDto)currentUser);
    }

    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
        if (user == null)
            return new CustomResponse(HttpStatusCode.BadRequest, $"There os not any user with id {id}");

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return new CustomResponse(HttpStatusCode.Accepted, (UserReadDto)user);
    }

}