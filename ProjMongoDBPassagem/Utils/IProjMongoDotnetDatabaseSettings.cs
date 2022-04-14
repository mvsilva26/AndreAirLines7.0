namespace ProjMongoDBPassagem.Utils
{
    public interface IProjMongoDotnetDatabaseSettings
    {

        string PassagemCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }


    }
}
