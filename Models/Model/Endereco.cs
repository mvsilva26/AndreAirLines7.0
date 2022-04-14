using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Model
{
    public class Endereco
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        [JsonProperty("Bairro")]
        public string Bairro { get; set; }

        [JsonProperty("Localidade")]
        public string Localidade { get; set; }
        public string Pais { get; set; }
        public string Cep { get; set; }

        [JsonProperty("Logradouro")]
        public string Logradouro { get; set; }

        [JsonProperty("Uf")]
        public string Uf { get; set; }

        [JsonProperty("Numero")]
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Continente { get; set; }

        public Endereco(string logradouro, string uf, string localidade, string bairro, string numero, string complemento, string cep)
        {

            Logradouro = logradouro;
            Uf = uf;
            Pais = "Brasil";
            Localidade = localidade;
            Bairro = bairro;
            Numero = numero;
            Complemento = complemento;
            Cep = cep;

        }

        public Endereco(string logradouro, string uf, string localidade, string bairro, string numero, string complemento, string cep, string pais, string continent)
        {

            Logradouro = logradouro;
            Uf = uf;
            Pais = pais;
            Localidade = localidade;
            Bairro = bairro;
            Numero = numero;
            Complemento = complemento;
            Cep = cep;
            Continente = continent;
            
        }

        public Endereco(string bairro, string localidade, string logradouro, string uf, string numero)
        {
            Bairro=bairro;
            Localidade=localidade;
            Logradouro=logradouro;
            Uf=uf;
            Numero=numero;
        }

        public Endereco()
        {


            
        }
    }
}
