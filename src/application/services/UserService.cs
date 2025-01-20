using Microsoft.AspNetCore.Mvc;
using System.Net;

public class UserService(IUserRepository repository) : IUserService
{
    private IUserRepository _repository = repository;

    public IActionResult ReadAllUsers()
    {
        var users = _repository.ReadAllUsers().Result.Select(user => (UserReadDto)user);

        return new CustomResponse(HttpStatusCode.Accepted, users);
    }

    public IActionResult ReadUsersById(Guid id)
    {
        var user = (UserReadDto)_repository.ReadUsersById(id).Result;
        if (user == null)
            return new CustomResponse(HttpStatusCode.BadRequest, $"User with {id} not found");

        return new CustomResponse(HttpStatusCode.Accepted, user);
    }

    public IActionResult CreateUser(UserCreateDto request)
    {
        if (request == null)
            return new CustomResponse(HttpStatusCode.BadRequest, "Request is null");

        var user = _repository.ReadUsersByUniqueAttributes(request).Result;
        if (user != null)
            return new CustomResponse(HttpStatusCode.BadRequest,
                $"Already exist an user with Username {request.Username} or Email {request.Email}");

        _repository.CreateUser(request);

        var response = (UserReadDto)(User)request;
        return new CustomResponse(HttpStatusCode.Created, response);
    }

    public IActionResult UpdateUser(Guid id, UserUpdateDto request)
    {
        if (request == null)
            return new CustomResponse(HttpStatusCode.BadRequest, "Request is null");

        var current = _repository.ReadUsersById(id).Result;
        if (current == null)
            return new CustomResponse(HttpStatusCode.BadRequest, $"User with {id} not found");

        var user = (UserReadDto)_repository.UpdateUser(current, request).Result;
        return new CustomResponse(HttpStatusCode.Accepted, user);
    }

    public IActionResult DeleteUser(Guid id)
    {
        var user = _repository.ReadUsersById(id).Result;
        if (user == null)
            return new CustomResponse(HttpStatusCode.BadRequest, $"There os not any user with id {id}");

        _repository.DeleteUser(user);

        var response = (UserReadDto)user;

        return new CustomResponse(HttpStatusCode.Accepted, response);
    }

}