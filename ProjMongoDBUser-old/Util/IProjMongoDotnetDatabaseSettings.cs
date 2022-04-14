namespace ProjMongoDBUser.Util
{
    public interface IProjMongoDotnetDatabaseSettings
    {
        string UserCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
