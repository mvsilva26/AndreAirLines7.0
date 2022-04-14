using Models.Model;
using MongoDB.Driver;
using Newtonsoft.Json;
using ProjMongoDBLog.Utils;
using RabbitMQ.Client;
using System.Collections.Generic;
using System.Text;

namespace ProjMongoDBLog.Service
{
    public class LogService
    {

        private readonly IMongoCollection<Log> _logs;

        private readonly ConnectionFactory _factory = new ConnectionFactory {HostName = "localhost" };
        private const string QUEUE_NAME = "messagelogs";

        public LogService(IProjMongoDotnetDatabaseSettings settings)
        {

            var log = new MongoClient(settings.ConnectionString);
            var database = log.GetDatabase(settings.DatabaseName);
            _logs = database.GetCollection<Log>(settings.LogCollectionName);

        }

        public List<Log> Get() =>
            _logs.Find(log => true).ToList();



        public Log Get(string id) =>
           _logs.Find<Log>(log => log.Id == id).FirstOrDefault();


        public Log Create(Log log)
        {
            using (var connection = _factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {

                    channel.QueueDeclare(
                        queue: QUEUE_NAME,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                        );

                    var stringfieldMessage = JsonConvert.SerializeObject(log);
                    var bytesMessage = Encoding.UTF8.GetBytes(stringfieldMessage);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: QUEUE_NAME,
                        basicProperties: null,
                        body: bytesMessage
                        );
                }
            }
            return log;
        }


        public void Update(string id, Log logIn) =>
            _logs.ReplaceOne(log => log.Id == id, logIn);

        public void Remove(Log logIn) =>
            _logs.DeleteOne(log => log.Id == logIn.Id);

        public void Remove(string id) =>
            _logs.DeleteOne(log => log.Id == id);








    }
}
