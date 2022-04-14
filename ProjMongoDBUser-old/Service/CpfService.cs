namespace ProjMongoDBUser.Service
{
    public class CpfService
    {

        public static bool CheckCpfDB(string cpf, UserService _passageiroService)
        {
            if (_passageiroService.GetCpf(cpf) != null)
            { return false; }
            else
            { return true; }

        }

    }
}
