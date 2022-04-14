namespace ProjMongoDBPassageiro.Utils
{
    public interface IProjMongoDotnetDatabaseSettings
    {
        string PassageiroCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }

    }
}
