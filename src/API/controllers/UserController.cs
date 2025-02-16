using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

public class UserController(IIdentityService identity) : ApiControllerBase
{
    [HttpGet]
    public async Task<IActionResult> ReadAllUsers()
    {
        var users = await identity.ReadAllUsersAsync();

        return new CustomResponse(HttpStatusCode.OK, users);
    }

    [HttpGet("whole/{id}")]
    public async Task<IActionResult> ReadWholeUserByIdAsync([FromRoute] Guid id)
    {
        var user = await identity.ReadWholeUserByIdAsync(id);

        return new CustomResponse(HttpStatusCode.OK, user);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ReadUsersById([FromRoute] Guid id)
    {
        var user = await identity.ReadUserByIdAsync(id);

        if (!user.Succeeded)
            return new CustomResponse(HttpStatusCode.BadRequest, user.Errors);

        return new CustomResponse(HttpStatusCode.OK, user);
    }

    [HttpGet("email/{email}")]
    public async Task<IActionResult> ReadUserByEmail([FromRoute] string email)
    {
        var user = await identity.ReadUserByEmailAsync(email);

        return new CustomResponse(HttpStatusCode.OK, user);
    }

    [HttpPost("signup")]
    public IActionResult SignUp([FromBody] UserSignUpRequest request)
    {
        if (request == null)
            return new CustomResponse(HttpStatusCode.BadRequest, $"{nameof(request)} is null");

        if (!ModelState.IsValid)
            return new CustomResponse(HttpStatusCode.BadRequest, $"{nameof(request)} is invalid");

        var response = identity.SignUpUserAsync(request);

        if (response.Result.Success)
            return new CustomResponse(HttpStatusCode.Created, response.Result);

        return new CustomResponse(HttpStatusCode.BadRequest, response.Result.Errors);
    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignInAsync([FromBody] UserSignInRequest request)
    {
        if (request == null)
            return new CustomResponse(HttpStatusCode.BadRequest, $"{nameof(request)} is null");

        if (!ModelState.IsValid)
            return new CustomResponse(HttpStatusCode.BadRequest, $"{nameof(request)} is invalid");

        var response = await identity.SignInUserAsync(request);

        if (response.Successed)
            return new CustomResponse(HttpStatusCode.Accepted, response);

        return new CustomResponse(HttpStatusCode.BadRequest, response.Errors);
    }

    [Authorize]
    [HttpPost("refresh-signin")]
    public async Task<IActionResult> RefreshSignInAsync([FromRoute] Guid id)
    {
        if (Guid.Empty == id)
            return new CustomResponse(HttpStatusCode.BadRequest, $"{nameof(id)} is null");

        var response = await identity.RefreshSignInUserAsync(id);

        if (response.Successed)
            return new CustomResponse(HttpStatusCode.Accepted, response);

        return new CustomResponse(HttpStatusCode.BadRequest, response.Errors);
    }

    [HttpPatch("email/{id}")]
    public async Task<IActionResult> ChangeEmail([FromRoute] Guid id, [FromBody] string newEmail)
    {
        if (newEmail == null)
            return new CustomResponse(HttpStatusCode.BadRequest, $"{nameof(newEmail)} is null");

        var user = await identity.ChangeEmailAsync(id, newEmail);

        if (!user.Succeeded)
            return new CustomResponse(HttpStatusCode.BadRequest, user.Errors);

        return new CustomResponse(HttpStatusCode.Accepted, user);
    }

    [HttpPatch("username/{id}")]
    public async Task<IActionResult> ChangeUsername([FromRoute] Guid id, [FromBody] string newUsername)
    {
        if (newUsername == null)
            return new CustomResponse(HttpStatusCode.BadRequest, $"{nameof(newUsername)} is null");

        var user = await identity.ChangeUsernameAsync(id, newUsername);

        if (!user.Succeeded)
            return new CustomResponse(HttpStatusCode.BadRequest, user.Errors);

        return new CustomResponse(HttpStatusCode.Accepted, user);
    }

    [HttpPatch("phone/{id}")]
    public async Task<IActionResult> ChangePhone([FromRoute] Guid id, [FromBody] string newPhoneNumber)
    {
        if (newPhoneNumber == null)
            return new CustomResponse(HttpStatusCode.BadRequest, $"{nameof(newPhoneNumber)} is null");

        var user = await identity.ChangePhoneAsync(id, newPhoneNumber);

        if (!user.Succeeded)
            return new CustomResponse(HttpStatusCode.BadRequest, user.Errors);

        return new CustomResponse(HttpStatusCode.Accepted, user);
    }

    [HttpPatch("password/{id}")]
    public async Task<IActionResult> ChangePassword([FromRoute] Guid id, [FromBody] ChangePasswordRequest request)
    {
        if (request == null || request.CurrentPassword == null || request.NewPassword == null)
            return new CustomResponse(HttpStatusCode.BadRequest, $"{nameof(request)} is null");

        var user = await identity.ChangePasswordAsync(id, request);

        if (!user.Succeeded)
            return new CustomResponse(HttpStatusCode.BadRequest, user.Errors);

        return new CustomResponse(HttpStatusCode.Accepted, user);
    }

    [HttpPatch("active/{id}")]
    public async Task<IActionResult> ChangeActiveUserById([FromRoute] Guid id)
    {
        var user = await identity.ChangeActiveUserByIdAsync(id);

        if (!user.Succeeded)
            return new CustomResponse(HttpStatusCode.BadRequest, user.Errors);

        return new CustomResponse(HttpStatusCode.Accepted, user);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
    {
        var user = await identity.DeleteUserByIdAsync(id);

        if (!user.Succeeded)
            return new CustomResponse(HttpStatusCode.BadRequest, user.Errors);

        return new CustomResponse(HttpStatusCode.Accepted, user);
    }
}
