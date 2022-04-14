namespace ProjMongoDBAeronave.Utils
{
    public interface IProjMongoDotnetDatabaseSettings
    {
        string AeronaveCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }

    }
}
