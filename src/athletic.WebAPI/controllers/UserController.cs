using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("users/")]
public class UserController
{

    [HttpGet]
    public string GetAllUsers()
    {
        return "Hello world";
    }
}