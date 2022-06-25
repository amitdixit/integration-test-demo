namespace UserApi.Models;

public class User
{
    public int UserId { get; init; }
    public string FullName { get; init; }
    public string UserName { get; init; }
    public string Password { get; init; }
    public string UserEmail { get; init; }
}
