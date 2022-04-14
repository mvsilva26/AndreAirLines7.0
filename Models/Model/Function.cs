    using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Model
{
    public class Function
    {


        public string Id { get; set; }
        public string Descricao { get; set; }
        public Permission Permission { get; set; }



    }
}
