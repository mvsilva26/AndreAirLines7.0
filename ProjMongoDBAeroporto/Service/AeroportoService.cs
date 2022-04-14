using Models.Model;
using MongoDB.Driver;
using ProjMongoDBAeroporto.Utils;
using System.Collections.Generic;

namespace ProjMongoDBAeroporto.Service
{
    public class AeroportoService
    {
        private readonly IMongoCollection<Aeroporto> _aeroportos;


        public AeroportoService(IProjMongoDotnetDatabaseSettings settings)
        {

            var aeroporto = new MongoClient(settings.ConnectionString);
            var database = aeroporto.GetDatabase(settings.DatabaseName);
            _aeroportos = database.GetCollection<Aeroporto>(settings.AeroportoCollectionName);

        }

        public List<Aeroporto> Get() =>
            _aeroportos.Find(aeroporto => true).ToList();


        public Aeroporto GetSigla(string Sigla) =>                                                        /////
           _aeroportos.Find<Aeroporto>(aeroporto => aeroporto.Sigla == Sigla).FirstOrDefault();          ////



        public Aeroporto Get(string id) =>
           _aeroportos.Find<Aeroporto>(aeroporto => aeroporto.Id == id).FirstOrDefault();

        public Aeroporto Create(Aeroporto aeroporto)
        {
            _aeroportos.InsertOne(aeroporto);
            return aeroporto;
        }

        public void Update(string id, Aeroporto aeroportoIn) =>
            _aeroportos.ReplaceOne(aeroporto => aeroporto.Id == id, aeroportoIn);

        public void Remove(Aeroporto aeroportoIn) =>
            _aeroportos.DeleteOne(aeroporto => aeroporto.Id == aeroportoIn.Id);

        public void Remove(string id) =>
            _aeroportos.DeleteOne(aeroporto => aeroporto.Id == id);



    }
}
