using System.Text.Json.Serialization;

public class UserGeneralResponse
{
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public bool Succeeded => Errors == null || Errors.Count == 0;
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public List<string>? Errors { get; set; }
    public Guid Id { get; set; }
    public string Email { get; set; }
    private string _userName = string.Empty;
    public string UserName
    {
        get
        {
            if (_userName == Email)
                return "Usuário sem nome";
            return _userName;
        }
        set
        {
            if (value == Email)
                _userName = Id.ToString()!.Substring(0, 5);

            _userName = value;
        }
    }
    private string _phoneNumber = string.Empty;

    public string PhoneNumber
    {
        get
        {
            if (string.IsNullOrEmpty(_phoneNumber))
                return "99 99999-9999";
            return _phoneNumber;
        }
        set
        {
            _phoneNumber = value;
        }
    }

    public UserGeneralResponse()
    {
        Id = Guid.NewGuid();
        Email = string.Empty;
        PhoneNumber = string.Empty;
        Errors = null;
    }
    public UserGeneralResponse(IEnumerable<string> errors)
    {
        Id = Guid.NewGuid();
        Email = string.Empty;
        PhoneNumber = string.Empty;
        Errors = errors.ToList();
    }

    public UserGeneralResponse(Guid id, string userName, string email, string phoneNumber)
    {
        Id = id;
        UserName = userName;
        Email = email;
        PhoneNumber = phoneNumber;
        Errors = null;
    }

    public void AddError(string error)
    {
        Errors ??= new List<string>();
        Errors.Add(error);
    }
}
