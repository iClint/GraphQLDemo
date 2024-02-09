using GraphQlDemo.GraphQl;
using GraphQlDemo.Models;
using GraphQlDemo.Services;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

builder.Services
    .AddSingleton(new MongoDbService(
        configuration
            .GetSection("MongoDbSettings")["ConnectionString"],
        configuration
            .GetSection("MongoDbSettings")["DatabaseName"]))
    .AddSingleton<DatabaseService<Employee>>(provider =>
    {
        var mongoDbService = provider.GetRequiredService<MongoDbService>();
        return new DatabaseService<Employee>(mongoDbService.GetDatabase(), "employees");
    })
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>();

var app = builder.Build();

app.MapGraphQL("/graphql");

app.Run();