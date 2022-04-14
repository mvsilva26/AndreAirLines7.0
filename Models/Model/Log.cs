using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Model
{
    public class Log
    {
        #region Properties
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string User { get; set; }
        public string Before { get; set; }
        public string After { get; set; }
        public string Operation { get; set; }
        public DateTime Date { get; set; }

        public Log(string user, string before, string after, string operation)
        {
            User = user;
            Before = before;
            After = after;
            Operation = operation;
        }


        #endregion





    }
}
