namespace ProjMongoDBPrecoBase.Util
{
    public interface IProjMongoDotnetDatabaseSettings
    {

        string PrecoBaseCollectionName { get; set; }
        string ClasseCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }

    }
}
