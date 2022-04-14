namespace ProjMongoDBLog.Utils
{
    public interface IProjMongoDotnetDatabaseSettings
    {

        string LogCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }


    }
}
