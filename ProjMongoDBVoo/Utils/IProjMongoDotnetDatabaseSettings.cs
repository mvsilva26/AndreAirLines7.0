namespace ProjMongoDBVoo.Utils
{
    public interface IProjMongoDotnetDatabaseSettings
    {
        string VooCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }

    }
}
