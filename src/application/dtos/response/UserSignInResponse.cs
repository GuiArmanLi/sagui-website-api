using System.Text.Json.Serialization;

public class UserSignInResponse
{
    public List<string> Errors { get; private set; }
    public bool Successed => Errors.Count == 0;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? AccessToken { get; private set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? RefreshToken { get; private set; }

    public UserSignInResponse() => Errors = new List<string>();

    public UserSignInResponse(string accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
        Errors = new List<string>();
    }

    public void AddError(string error) => Errors.Add(error);
}