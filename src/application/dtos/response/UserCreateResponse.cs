public class UserCreateResponse
{
    public bool Success { get; private set; }
    public List<string> Errors { get; private set; }

    public UserCreateResponse(bool sucess = true) : this() => Success = sucess;

    public UserCreateResponse() =>
        Errors = new List<string>();

    public void AddErros(IEnumerable<string> errors) =>
        Errors.AddRange(errors);
}