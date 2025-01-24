using System.Text.Json.Serialization;

public class UserLoginResponse
{
    public List<string> Errors { get; private set; }
    public bool Success => Errors.Count == 0;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string AccessToken { get; private set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string RefreshToken { get; private set; }

    public UserLoginResponse() => Errors = new List<string>();

    public UserLoginResponse(string accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }

    public void AddError(string error) => Errors.Add(error);
}