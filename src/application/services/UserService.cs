using Microsoft.AspNetCore.Mvc;
using System.Net;

public class UserService(IUserRepository repository) : IUserService
{
    private IUserRepository _repository = repository;

    public IActionResult ReadAllUsers()
    {
        var users = _repository.ReadAllUsers().Result.Select(UserResponse.ConvertToUserResponse);

        return new CustomResponse(HttpStatusCode.Accepted, users);
    }

    public IActionResult ReadUsersById(Guid id)
    {
        var user = UserResponse.ConvertToUserResponse(
            _repository.ReadUserById(id).Result
            );
        if (user == null)
            return new CustomResponse(HttpStatusCode.BadRequest, $"User with {id} not found");

        return new CustomResponse(HttpStatusCode.Accepted, user);
    }

    public IActionResult CreateUser(UserCreateRequest request)
    {
        if (request == null)
            return new CustomResponse(HttpStatusCode.BadRequest, "Request is null");

        var user = _repository.ReadUserByEmail(request.Email).Result;
        if (user != null)
            return new CustomResponse(HttpStatusCode.BadRequest, $"Already exist an user with Email {request.Email}");

        user = UserCreateRequest.ConvertToEntity(request);
        _repository.CreateUser(user);


        return new CustomResponse(HttpStatusCode.Created, user);
    }

    [HttpPost("login")]
    public IActionResult LoginUser(UserLoginRequest request)
    {
        return null;
    }

    public IActionResult UpdateUser(Guid id, UserUpdateRequest request)
    {
        if (request == null)
            return new CustomResponse(HttpStatusCode.BadRequest, "Request is null");

        var current = _repository.ReadUserById(id).Result;
        if (current == null)
            return new CustomResponse(HttpStatusCode.BadRequest, $"User with {id} not found");

        var user = UserResponse.ConvertToUserResponse(
            _repository.UpdateUser(current, UserUpdateRequest.ConvertToEntity(request)).Result
        );
        return new CustomResponse(HttpStatusCode.Accepted, user);
    }

    public IActionResult DeleteUser(Guid id)
    {
        var user = _repository.ReadUserById(id).Result;
        if (user == null)
            return new CustomResponse(HttpStatusCode.BadRequest, $"There os not any user with id {id}");

        _repository.DeleteUser(user);

        var response = UserResponse.ConvertToUserResponse(user);
        return new CustomResponse(HttpStatusCode.Accepted, response);
    }

}