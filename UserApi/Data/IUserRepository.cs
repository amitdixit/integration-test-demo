using System.Collections.Generic;
using UserApi.Models;

namespace UserApi.Data;

internal interface IUserRepository
{
    Task<User>GetUserByCredentials(string userName, string password);
    Task<IEnumerable<User>> GetUsers();
}