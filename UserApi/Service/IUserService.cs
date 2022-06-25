using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserApi.Data;
using UserApi.Models;

namespace UserApi.Service;

public interface IUserService
{
    Task<string> VaildateUser(string userName, string password);
    Task<IEnumerable<User>> GetUsers();
}


internal class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<User>> GetUsers()
    {
       return await _userRepository.GetUsers();
    }

    public async Task<string> VaildateUser(string userName, string password)
    {
        var user = await _userRepository.GetUserByCredentials(userName, password);

        if (user is not null)
        {
            return GenerateToken(user);
        }

        return null;
    }

    private string GenerateToken(User user)
    {
        var claims = new Claim[] {
                           new Claim (JwtRegisteredClaimNames.Sub, user.UserName),
                           new Claim (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                           new Claim(ClaimTypes.Name, user.UserName),
                           new Claim(ClaimTypes.Role, "user"),
                           new Claim("FullName", user.FullName),
                           new Claim(ClaimTypes.PrimarySid, user.UserId.ToString())
                       };


        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mysupersecretkey123!"));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var now = DateTime.UtcNow;//.ToLocalTime();
        claims.Append(new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUniversalTime().ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64));

        // Create the JWT and write it to a string
        var jwt = new JwtSecurityToken(
            issuer: "MyTestApp",
            audience: "TokenAudience",
            claims: claims,
            notBefore: now,
            expires: now.Add(TimeSpan.FromMinutes(1)),
            signingCredentials: credentials);


        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}