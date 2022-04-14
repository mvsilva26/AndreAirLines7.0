using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Model
{
    public class User : Person
    {

        #region Properties
        public string Senha { get; set; }
        public string Login { get; set; }
        public string Setor { get; set; }
        public Function Function { get; set; }

        #endregion

    }
}
