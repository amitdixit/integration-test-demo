using UserApi.Models;

namespace UserApi.Data;

internal class UserRepository : IUserRepository
{
    public async Task<User> GetUserByCredentials(string userName, string password)
    {
        await Task.CompletedTask;
        return UserData.Users.FirstOrDefault(u => u.UserName == userName && u.Password == password);
    }

    public async Task<IEnumerable<User>> GetUsers()
    {
        await Task.CompletedTask;

        return UserData.Users;
    }

    private class UserData
    {
        public static List<User> Users
        {
            get
            {
                return new List<User>()
            {
                new User{UserId = 1, FullName = "Amit Dixit",UserEmail="amit.d@gmail.com", UserName ="amit.d", Password = "Password1"},
                new User{UserId = 2, FullName = "Sandhya Dixit",UserEmail="sandhya.d@gmail.com", UserName ="sandhya.d", Password = "Password1"},
                new User{UserId = 3, FullName = "Aashita Dixit",UserEmail="aashita.d@gmail.com", UserName ="aashita.d", Password = "Password1"},
                new User{UserId = 4, FullName = "Pinaak Dixit",UserEmail="pinaak.d@gmail.com", UserName ="pinaak.d", Password = "Password1"},
            };
            }
        }
    }
}


