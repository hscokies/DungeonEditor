namespace Domain.Users;

public class User
{
    public string UserName { get; set; }
    public ushort Tokens { get; private set; }
}