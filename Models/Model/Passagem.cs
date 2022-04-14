using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Model
{
    public class Passagem
    {
        #region Properties
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Voo Voo { get; set; }
        public Passageiro Passageiro { get; set; }
        public decimal TotalPreco { get; set; }
        public Classe Classe { get; set; }
        public PrecoBase PrecoBase { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public decimal PromocaoPorcentagem { get; set; }

        public string LoginUser { get; set; }

        #endregion

    }
}
