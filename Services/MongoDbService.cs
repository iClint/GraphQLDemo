namespace GraphQlDemo.Services;

using MongoDB.Driver;

public class MongoDbService
{
    private readonly IMongoDatabase _mongoDatabase;

    public MongoDbService(string connectionString, string databaseName)
    {
        IMongoClient mongoClient = new MongoClient(connectionString);
        _mongoDatabase = mongoClient.GetDatabase(databaseName);
    }

    public IMongoDatabase GetDatabase()
    {
        return _mongoDatabase;
    }
}
