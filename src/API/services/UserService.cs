using System.Net;
using Microsoft.AspNetCore.Mvc;

public class UserService : IUserService
{
    private static List<User> _users = new List<User>();

    public IActionResult ReadAllUsers()
    {
        var response = _users.Select(user => (UserReadDto)user).ToList();

        return new CustomResponse(HttpStatusCode.Accepted, response);
    }

    public IActionResult ReadUsersById(Guid id)
    {
        var user = _users.FirstOrDefault(user => user.Id == id);
        if (user == null)
            return new CustomResponse(HttpStatusCode.BadRequest, $"User with {id} not found");

        return new CustomResponse(HttpStatusCode.Accepted, (UserReadDto)user);
    }

    public IActionResult CreateUser(UserCreateDto request)
    {
        if (request == null)
            return new CustomResponse(HttpStatusCode.BadRequest, "Request is null");

        var user = _users.FirstOrDefault(
            user => user.Username == request.Username
            || user.Email == request.Email);

        if (user != null)
            return new CustomResponse(
                HttpStatusCode.BadRequest,
                $"Already exist an user with Username {request.Username} or Email {request.Email}"
            );

        _users.Add(request);
        return new CustomResponse(HttpStatusCode.Created, request); //Retornar DTO
    }

    public IActionResult UpdateUser(Guid id, UserUpdateDto request)
    {
        if (request == null)
            return new CustomResponse(HttpStatusCode.BadRequest, "Request is null");

        var currentUser = _users.FirstOrDefault(user => user.Id == id);
        if (currentUser == null)
            return new CustomResponse(HttpStatusCode.BadRequest, $"User with {id} not found");

        //Utils.UpdateObject(currentUser, updatedUser);

        return new CustomResponse(HttpStatusCode.Accepted, (UserReadDto)currentUser);
    }

    public IActionResult DeleteUser(Guid id)
    {
        var user = _users.Find(user => user.Id == id);
        if (user == null)
            return new CustomResponse(HttpStatusCode.BadRequest, $"There os not any user with id {id}");

        _users.Remove(user);

        return new CustomResponse(HttpStatusCode.Accepted, (UserReadDto)user);
    }

}