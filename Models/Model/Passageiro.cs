using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Model
{
    public class Passageiro : Person
    {
        #region Properties
        public string CodPassaporte { get; set; }

        #endregion
    }

}
