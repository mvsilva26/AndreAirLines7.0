namespace ProjMongoDBAeronave.Service
{
    public class PlacaService
    {
		public static bool VerificaAeronaveSigla(string placa, AeronaveService _aeronave)
		{
			if (_aeronave.GetAeronavePlaca(placa) != null)
				return false;
			return true;
		}


	}
}
