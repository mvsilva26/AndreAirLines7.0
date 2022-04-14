using Models.Model;
using MongoDB.Driver;
using ProjMongoDBVoo.Utils;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ProjMongoDBVoo.Service
{
    public class VooService
    {

        private readonly IMongoCollection<Voo> _voos;

        public VooService(IProjMongoDotnetDatabaseSettings settings)
        {

            var voo = new MongoClient(settings.ConnectionString);
            var database = voo.GetDatabase(settings.DatabaseName);
            _voos = database.GetCollection<Voo>(settings.VooCollectionName);

        }

        public Voo GetVoo(string origin, string destination) =>
           _voos.Find<Voo>(voo => voo.Origem.Sigla == origin && voo.Destino.Sigla == destination).FirstOrDefault();

        public List<Voo> Get() =>
            _voos.Find(voo => true).ToList();

        public Voo Get(string id) =>
           _voos.Find<Voo>(voo => voo.Id == id).FirstOrDefault();

        public Voo Create(Voo voo)
        {
            _voos.InsertOne(voo);
            return voo;

        }

        public void Update(string id, Voo vooIn) =>
            _voos.ReplaceOne(voo => voo.Id == id, vooIn);

        public void Remove(Voo vooIn) =>
            _voos.DeleteOne(voo => voo.Id == vooIn.Id);

        public void Remove(string id) =>
            _voos.DeleteOne(voo => voo.Id == id);





    }
}
