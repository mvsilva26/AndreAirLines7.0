using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Model
{
    public class BaseResponse
    {

        public bool Sucess { get; set; }
        public object Result { get; set; }
        public List<string> Erros { get; set; }

        public BaseResponse()
        {
            Erros = new List<string>();
            Sucess = true;
        }

        public void ConnectResult(object result)
        {
            Result = result;
            Sucess = true;
        }



        public void Connecterro(string erro)
        {
            Erros.Add(erro);
            Sucess = false;

        }

    }
}
