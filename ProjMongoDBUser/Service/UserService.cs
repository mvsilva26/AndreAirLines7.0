using Models.Model;
using MongoDB.Driver;
using ProjMongoDBUser.Util;
using System.Collections.Generic;

namespace ProjMongoDBUser.Service
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(IProjMongoDotnetDatabaseSettings settings)
        {

            var user = new MongoClient(settings.ConnectionString);
            var database = user.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<User>(settings.UserCollectionName);

        }

        public User GetLogin(string login) =>
            _users.Find<User>(user => user.Login == login).FirstOrDefault();




        public User GetLogin(User u) =>
            _users.Find(user => user.Login == u.Login && user.Senha == u.Senha).FirstOrDefault();



        public User GetCpf(string cpf) =>
            _users.Find<User>(user => user.Cpf == cpf).FirstOrDefault();
        public List<User> Get() =>
           _users.Find(aero => true).ToList();

        public User Get(string id) =>
           _users.Find<User>(us => us.Id == id).FirstOrDefault();

        public User Create(User user)
        {
            _users.InsertOne(user);
            return user;
        }

        public void Update(string id, User userIn) =>
            _users.ReplaceOne(us => us.Id == id, userIn);

        public void Remove(User userIn) =>
            _users.DeleteOne(user => user.Id == userIn.Id);

        public void Remove(string id) =>
            _users.DeleteOne(user => user.Id == id);

    }
}
