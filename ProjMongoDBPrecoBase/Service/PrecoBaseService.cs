using Models.Model;
using MongoDB.Driver;
using ProjMongoDBPrecoBase.Util;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ProjMongoDBPrecoBase.Service
{
    public class PrecoBaseService
    {

        private readonly IMongoCollection<PrecoBase> _precosBases;

        public PrecoBaseService(IProjMongoDotnetDatabaseSettings settings)
        {

            var precobase = new MongoClient(settings.ConnectionString);
            var database = precobase.GetDatabase(settings.DatabaseName);
            _precosBases = database.GetCollection<PrecoBase>(settings.PrecoBaseCollectionName);

        }

        public PrecoBase GetPrecoBase(string origin, string destination) =>
          _precosBases.Find<PrecoBase>(precoBase => precoBase.Origem.Sigla == origin && precoBase.Destino.Sigla == destination).FirstOrDefault();


        public List<PrecoBase> Get() =>
            _precosBases.Find(precBase => true).ToList();

        public PrecoBase Get(string id) =>
           _precosBases.Find<PrecoBase>(precBase => precBase.Id == id).FirstOrDefault();

        public PrecoBase Create(PrecoBase precoBase)
        {

            _precosBases.InsertOne(precoBase);
            return precoBase;


        }

        public void Update(string id, PrecoBase precoBaseIn) =>
           _precosBases.ReplaceOne(precoBase => precoBase.Id == id, precoBaseIn);

        public void Remove(PrecoBase precoBaseIn) =>
            _precosBases.DeleteOne(precoBase => precoBase.Id == precoBaseIn.Id);

        public void Remove(string id) =>
            _precosBases.DeleteOne(precoBase => precoBase.Id == id);




    }
}
