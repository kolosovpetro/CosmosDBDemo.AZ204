using CosmosDBDemo.AZ204.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var databaseConfig = builder.Configuration.GetSection("DatabaseConfiguration").Get<DatabaseConfiguration>();
builder.Services.AddScoped(_ => databaseConfig);

var options = new CosmosClientOptions
{
    SerializerOptions = new CosmosSerializationOptions
    {
        PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase,
    }
};

var cosmosClient = new CosmosClient(databaseConfig.EndpointUrl, databaseConfig.PrimaryKey, options);
builder.Services.AddScoped(_ => cosmosClient);

var cosmosDatabase = await cosmosClient.CreateDatabaseIfNotExistsAsync(databaseConfig.DatabaseId);
builder.Services.AddScoped(_ => cosmosDatabase.Database);

var cosmosContainer = await cosmosDatabase.Database.CreateContainerIfNotExistsAsync(
    databaseConfig.ContainerId,
    databaseConfig.PartitionKey);
builder.Services.AddScoped(_ => cosmosContainer.Container);


var context = new MoviesDataContext(cosmosClient, cosmosDatabase, cosmosContainer, databaseConfig);

builder.Services.AddScoped<IMoviesDataContext>(_ => context);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();