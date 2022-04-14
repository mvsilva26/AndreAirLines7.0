namespace ProjMongoDBAeroporto.Utils
{
    public interface IProjMongoDotnetDatabaseSettings
    {
        string AeroportoCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }


    }
}
