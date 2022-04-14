using Models.Model;
using MongoDB.Driver;
using ProjMongoDBPassageiro.Utils;
using System.Collections.Generic;

namespace ProjMongoDBPassageiro.Service
{
    public class PassageiroService
    {

        private readonly IMongoCollection<Passageiro> _passageiros;

        public PassageiroService(IProjMongoDotnetDatabaseSettings settings)
        {

            var passageiro = new MongoClient(settings.ConnectionString);
            var database = passageiro.GetDatabase(settings.DatabaseName);
            _passageiros = database.GetCollection<Passageiro>(settings.PassageiroCollectionName);

        }

        public List<Passageiro> Get() =>
            _passageiros.Find(person => true).ToList();

        public Passageiro GetCpf(string Cpf) =>
           _passageiros.Find<Passageiro>(person => person.Cpf == Cpf).FirstOrDefault();


        public Passageiro Get(string id) =>
           _passageiros.Find<Passageiro>(person => person.Id == id).FirstOrDefault();

        public Passageiro Create(Passageiro passageiro)
        {
            _passageiros.InsertOne(passageiro);
            return passageiro;
        }

        public void Update(string id, Passageiro passageiroIn) =>
            _passageiros.ReplaceOne(passageiro => passageiro.Id == id, passageiroIn);

        public void Remove(Passageiro passageiroIn) =>
            _passageiros.DeleteOne(passageiro => passageiro.Id == passageiroIn.Id);

        public void Remove(string id) =>
            _passageiros.DeleteOne(passageiro => passageiro.Id == id);



    }
}
