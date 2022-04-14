using Models.Model;
using MongoDB.Driver;
using ProjMongoDBPassagem.Utils;
using System.Collections.Generic;

namespace ProjMongoDBPassagem.Service
{
    public class PassagemService
    {

        private readonly IMongoCollection<Passagem> _passagens;


        public PassagemService(IProjMongoDotnetDatabaseSettings settings)
        {

            var passagem = new MongoClient(settings.ConnectionString);
            var database = passagem.GetDatabase(settings.DatabaseName);
            _passagens = database.GetCollection<Passagem>(settings.PassagemCollectionName);

        }

        public List<Passagem> Get() =>
            _passagens.Find(passagem => true).ToList();


        public Passagem Get(string id) =>
           _passagens.Find<Passagem>(passagem => passagem.Id == id).FirstOrDefault();


        public Passagem Create(Passagem passagem)
        {
            _passagens.InsertOne(passagem);
            return passagem;
        }

        public void Update(string id, Passagem passagemIn) =>
            _passagens.ReplaceOne(passagem => passagem.Id == id, passagemIn);

        public void Remove(Passagem passagemIn) =>
            _passagens.DeleteOne(passagem => passagem.Id == passagemIn.Id);

        public void Remove(string id) =>
            _passagens.DeleteOne(passagem => passagem.Id == id);





    }
}
