using Models.Model;
using MongoDB.Driver;
using ProjMongoDBPrecoBase.Util;
using System.Collections.Generic;

namespace ProjMongoDBPrecoBase.Service
{
    public class ClasseService
    {


        private readonly IMongoCollection<Classe> _classes;

        public ClasseService(IProjMongoDotnetDatabaseSettings settings)
        {

            var classe = new MongoClient(settings.ConnectionString);
            var database = classe.GetDatabase(settings.DatabaseName);
            _classes = database.GetCollection<Classe>(settings.ClasseCollectionName);

        }


        public List<Classe> Get() =>
            _classes.Find(classe => true).ToList();

        public Classe Get(string id) =>
           _classes.Find<Classe>(classe => classe.Id == id).FirstOrDefault();

        public Classe Create(Classe classe)
        {
            _classes.InsertOne(classe);
            return classe;
        }

        public void Update(string id, Classe classeIn) =>
            _classes.ReplaceOne(classe => classe.Id == id, classeIn);

        public void Remove(Classe classeIn) =>
            _classes.DeleteOne(classe => classe.Id == classeIn.Id);

        public void Remove(string id) =>
            _classes.DeleteOne(classe => classe.Id == id);




    }
}
