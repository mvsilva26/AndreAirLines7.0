

using ExampleJWTAuthentication.ModelExample;
using System.Collections.Generic;
using System.Linq;

namespace ExampleJWTAuthentication.Repositories
{
    public class UserRepository
    {

        public static UserExample Get(string username, string password)
        {
            var users = new List<UserExample>();
            users.Add(new UserExample { Id = 1, Username = "admin", Password = "admin", Role = "manager" });
            users.Add(new UserExample { Id = 2, Username = "user", Password = "user", Role = "employee" });
            return users.Where(x => x.Username.ToLower() == username.ToLower() && x.Password == password).FirstOrDefault();
        }



    }
}
