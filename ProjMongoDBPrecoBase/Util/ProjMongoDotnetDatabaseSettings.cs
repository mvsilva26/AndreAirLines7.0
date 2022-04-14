namespace ProjMongoDBPrecoBase.Util
{
    public class ProjMongoDotnetDatabaseSettings : IProjMongoDotnetDatabaseSettings
    {

        public string PrecoBaseCollectionName { get; set; }
        public string ClasseCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }


    }
}
