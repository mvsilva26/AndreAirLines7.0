using APILog.Util;
using Models.Model;
using MongoDB.Driver;

namespace APILog.Service
{
    public class LogService
    {

        private readonly IMongoCollection<Log> _logs;

        

        public LogService(IProjMongoDotnetDatabaseSettings settings)
        {

            var log = new MongoClient(settings.ConnectionString);
            var database = log.GetDatabase(settings.DatabaseName);
            _logs = database.GetCollection<Log>(settings.LogCollectionName);

        }


        


        public Log Create(Log log)
        {
            _logs.InsertOne(log);
            return log;
        }




    }
}
