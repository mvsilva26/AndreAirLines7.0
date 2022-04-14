using Models.Model;
using MongoDB.Driver;
using ProjMongoDBAeronave.Utils;
using System.Collections.Generic;

namespace ProjMongoDBAeronave.Service
{
    public class AeronaveService
    {

        private readonly IMongoCollection<Aeronave> _aeronaves;

       
        public AeronaveService(IProjMongoDotnetDatabaseSettings settings)
        {

            var aeronave = new MongoClient(settings.ConnectionString);
            var database = aeronave.GetDatabase(settings.DatabaseName);
            _aeronaves = database.GetCollection<Aeronave>(settings.AeronaveCollectionName);

        }

        public List<Aeronave> Get() =>
            _aeronaves.Find(aeronave => true).ToList();


   


        public Aeronave GetAeronavePlaca(string placa) =>
           _aeronaves.Find<Aeronave>(aeronave => aeronave.Placa == placa).FirstOrDefault();



        public Aeronave Get(string id) =>
           _aeronaves.Find<Aeronave>(aeronave => aeronave.Id == id).FirstOrDefault();

        public Aeronave Create(Aeronave aeronave)
        {
            _aeronaves.InsertOne(aeronave);
            return aeronave;
        }

        public void Update(string id, Aeronave aeronaveIn) =>
            _aeronaves.ReplaceOne(aeronave => aeronave.Id == id, aeronaveIn);

        public void Remove(Aeronave aeronaveIn) =>
            _aeronaves.DeleteOne(aeronave => aeronave.Id == aeronaveIn.Id);

        public void Remove(string id) =>
            _aeronaves.DeleteOne(aeronave => aeronave.Id == id);



    }       
}
