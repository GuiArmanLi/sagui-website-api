
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

public class IdentityService : IIdentityService
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly JwtOptions _jwt;

    public IdentityService(
        SignInManager<User> signInManager,
        UserManager<User> userManager,
        IOptions<JwtOptions> options
    )
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _jwt = options.Value;
    }

    public async Task<IEnumerable<UserGeneralResponse>> ReadAllUsersAsync()
    {
        return await _userManager.Users.Where(user => user.EmailConfirmed).Select(user => new UserGeneralResponse
        (
            Guid.Parse(user.Id),
            user.UserName ?? string.Empty,
            user.Email ?? string.Empty,
            user.PhoneNumber ?? string.Empty
        )).ToListAsync();
    }

    public async Task<IEnumerable<UserGeneralResponse>> ReadAllActiveUsersAsync()
    {
        return await _userManager.Users.Where(user => user.Active).Select(user => new UserGeneralResponse
        (
            Guid.Parse(user.Id),
            user.UserName ?? string.Empty,
            user.Email ?? string.Empty,
            user.PhoneNumber ?? string.Empty
        )).ToListAsync();
    }

    public async Task<object> ReadWholeUserByIdAsync(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());

        if (user == null)
            return new UserGeneralResponse(new List<string> { "User not found" });

        return user;
    }

    public async Task<UserGeneralResponse> ReadUserByIdAsync(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());

        var response = new UserGeneralResponse();

        if (user == null)
        {
            response.AddError("User not found");

            return response;
        }

        Utils.MergeObjects<User, UserGeneralResponse>(user, response);

        return response;
    }

    public async Task<UserGeneralResponse> ReadUserByEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        var response = new UserGeneralResponse();

        if (user == null)
        {
            response.AddError("User not found");

            return response;
        }

        Utils.MergeObjects<User, UserGeneralResponse>(user, response);

        return response;
    }

    public async Task<UserSignUpResponse> SignUpUserAsync(UserSignUpRequest request)
    {
        var user = new User
        {
            UserName = request.Email,
            Email = request.Email,
            EmailConfirmed = true,
            Active = true,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (result.Succeeded)
            await _userManager.SetLockoutEnabledAsync(user, false);

        var response = new UserSignUpResponse(result.Succeeded);
        if (!result.Succeeded)
            response.AddErros(result.Errors.Select(error => error.Description));

        return response;
    }

    public async Task<UserSignInResponse> SignInUserAsync(UserSignInRequest request)
    {
        var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, true);

        var response = new UserSignInResponse();

        if (result.Succeeded)
            response = await GenerateCredentials(request.Email);

        if (!result.Succeeded)
        {
            if (result.IsLockedOut)
                response.AddError("User is locked out.");
            else if (result.IsNotAllowed)
                response.AddError("User is not allowed.");
            else if (result.RequiresTwoFactor)
                response.AddError("User requires two factor");
            else
                response.AddError("Email or password are incorrect");
        }

        return response;
    }

    public async Task<UserSignInResponse> RefreshSignInUserAsync(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        var response = new UserSignInResponse();

        if (await _userManager.IsLockedOutAsync(user!))
            response.AddError("User is locked out");
        else if (!await _userManager.IsEmailConfirmedAsync(user!))
            response.AddError("The user must confirm the email before login");

        if (response.Successed)
            return await GenerateCredentials(user!.Email!);

        return response;
    }

    public async Task<UserGeneralResponse> ChangeEmailAsync(Guid id, string newEmail)
    {
        var user = await _userManager.FindByIdAsync(id.ToString()!);

        var response = new UserGeneralResponse();

        if (user == null)
        {
            response.AddError("User not found");

            return response;
        }

        var anotherUserEmail = await _userManager.FindByEmailAsync(newEmail);
        if (anotherUserEmail != null)
        {
            response.AddError("Email already in use");

            return response;
        }

        user.Email = newEmail;
        user.NormalizedEmail = newEmail.ToUpper();

        user.UpdatedAt = DateTime.Now;
        await _userManager.UpdateAsync(user);

        Utils.MergeObjects<User, UserGeneralResponse>(user, response);

        return response;
    }

    public async Task<UserGeneralResponse> ChangeUsernameAsync(Guid id, string newUsername)
    {
        var user = await _userManager.FindByIdAsync(id.ToString()!);

        var response = new UserGeneralResponse();

        if (user == null)
        {
            response.AddError("User not found");

            return response;
        }

        var anotherUsername = await _userManager.FindByNameAsync(newUsername);
        if (anotherUsername != null)
        {
            response.AddError("Username already in use");

            return response;
        }

        user.UserName = newUsername;
        user.NormalizedUserName = newUsername.ToUpper();

        user.UpdatedAt = DateTime.Now;
        await _userManager.UpdateAsync(user);

        Utils.MergeObjects<User, UserGeneralResponse>(user, response);

        return response;
    }

    public async Task<UserGeneralResponse> ChangePhoneAsync(Guid id, string newPhoneNumber)
    {
        var user = await _userManager.FindByIdAsync(id.ToString()!);

        var response = new UserGeneralResponse();

        if (user == null)
        {
            response.AddError("User not found");

            return response;
        }

        user.PhoneNumber = newPhoneNumber;

        user.UpdatedAt = DateTime.Now;
        await _userManager.UpdateAsync(user);

        Utils.MergeObjects<User, UserGeneralResponse>(user, response);

        return response;
    }

    public async Task<UserGeneralResponse> ChangePasswordAsync(Guid id, ChangePasswordRequest request)
    {
        var user = await _userManager.FindByIdAsync(id.ToString()!);

        var response = new UserGeneralResponse();

        if (user == null)
        {
            response.AddError("User not found");

            return response;
        }

        if (!await _userManager.CheckPasswordAsync(user, request.CurrentPassword))
        {
            response.AddError("Current password is incorrect");

            return response;
        }

        user.UpdatedAt = DateTime.Now;
        await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

        Utils.MergeObjects<User, UserGeneralResponse>(user, response);

        return response;
    }

    public async Task<UserGeneralResponse> ChangeActiveUserByIdAsync(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());

        var response = new UserGeneralResponse();

        if (user == null)
        {
            response.AddError("User not found");

            return response;
        }

        user.Active = !user.Active;

        user.UpdatedAt = DateTime.Now;
        await _userManager.UpdateAsync(user);

        Utils.MergeObjects<User, UserGeneralResponse>(user, response);

        return response;
    }

    public async Task<UserGeneralResponse> DeleteUserByIdAsync(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());

        var response = new UserGeneralResponse();

        if (user == null)
        {
            response.AddError("User not found");

            return response;
        }

        await _userManager.DeleteAsync(user);

        Utils.MergeObjects<User, UserGeneralResponse>(user, response);

        return response;
    }

    private async Task<UserSignInResponse> GenerateCredentials(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        var accessTokenClaim = await GetAllClaimsAsync(user!, addClaimsUser: true);
        var refreshTokenClaim = await GetAllClaimsAsync(user!, addClaimsUser: false);

        var expirationDateAccessToken = DateTime.Now.AddSeconds(_jwt.AccessTokenExpiration);
        var expirationDateRefreshToken = DateTime.Now.AddSeconds(_jwt.RefreshTokenExpiration);

        var accessToken = GenerateToken(accessTokenClaim, expirationDateAccessToken);
        var refreshToken = GenerateToken(refreshTokenClaim, expirationDateRefreshToken);

        return new UserSignInResponse(
            accessToken,
            refreshToken
        );
    }

    private async Task<List<Claim>> GetAllClaimsAsync(User user, bool addClaimsUser)
    {
        var claims = new List<Claim>();

        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email!));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, DateTime.Now.ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString()));

        if (addClaimsUser)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            claims.AddRange(userClaims);
            foreach (var role in roles)
                claims.Add(new Claim("role", role));
        }

        return claims;
    }

    private string GenerateToken(List<Claim> claims, DateTime expirationDate)
    {
        var jwt = new JwtSecurityToken(
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            claims: claims,
            notBefore: DateTime.Now,
            expires: expirationDate,
            signingCredentials: _jwt.SigningCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }


}