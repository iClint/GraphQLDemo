using GraphQlDemo.GraphQl;
using GraphQlDemo.Models;
using GraphQlDemo.Services;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var mongoDbSettings = configuration.GetSection("MongoDbSettings");
var mongoConnectionString = mongoDbSettings["ConnectionString"];
var mongoDatabaseName = mongoDbSettings["DatabaseName"];

var mongoClient = new MongoClient(mongoConnectionString);
var mongoDatabase = mongoClient.GetDatabase(mongoDatabaseName);

Console.WriteLine($"MongoDB Connection String: {mongoConnectionString}");
Console.WriteLine($"MongoDB Database Name: {mongoDatabaseName}");

builder.Services
    .AddSingleton<IMongoClient>(mongoClient)
    .AddSingleton<IMongoDatabase>(mongoDatabase)
    .AddSingleton<DatabaseService<Employee>>(_ =>
    {
        var connectionString = configuration.GetSection("MongoDbSettings")["ConnectionString"];
        return new DatabaseService<Employee>(mongoDatabase,"employees");
    })    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>();

var app = builder.Build();

app.MapGraphQL("/graphql");

app.Run();