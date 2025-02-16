public class UserSignUpResponse
{
    public bool Success { get; private set; }
    public List<string> Errors { get; private set; }

    public UserSignUpResponse(bool sucess = true) : this() => Success = sucess;

    public UserSignUpResponse() =>
        Errors = new List<string>();

    public void AddErros(IEnumerable<string> errors) =>
        Errors.AddRange(errors);
}