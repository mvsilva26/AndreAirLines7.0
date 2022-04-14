using IngestaoAirport.Service;
using Models.ModelSQL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngestaoAirport
{
    public class ArquivoReaderCSV
    {

        public static void ReadJson()
        {
            string pathFile = @"C:\Users\Michael Silva\Documents\Exercicios\Web\Aula04-04-2022\Dados.csv";
            StreamReader reader = new StreamReader(pathFile);
            string line = reader.ReadLine();
            while(line != null)
            {
                var atributes = line.Split(';');
                new AirportService().Add(new Airport(atributes[0], atributes[1], atributes[2], atributes[3]));
                line = reader.ReadLine();
            }
           
        }

        //public static List<Airport> ReadCSV()
        //{
        //    string pathFile = @"C:\Users\Michael Silva\Documents\Exercicios\Web\Aula04-04-2022\csvjson.json";
        //    StreamReader reader = new StreamReader(pathFile);
            
        //}








    }
}
