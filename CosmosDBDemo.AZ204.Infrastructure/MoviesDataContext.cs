using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CosmosDBDemo.AZ204.Domain;
using CosmosDBDemo.AZ204.DTO;
using Microsoft.Azure.Cosmos;

namespace CosmosDBDemo.AZ204.Infrastructure;

public class MoviesDataContext : IMoviesDataContext
{
    private readonly CosmosClient _cosmosClient;
    private readonly Database _database;
    private readonly Container _container;
    private readonly DatabaseConfiguration _databaseConfiguration;

    public MoviesDataContext(
        CosmosClient cosmosClient,
        Database database,
        Container container,
        DatabaseConfiguration databaseConfiguration)
    {
        _cosmosClient = cosmosClient;
        _database = database;
        _container = container;
        _databaseConfiguration = databaseConfiguration;
    }

    public async Task<MovieEntity> InsertAsync(CreateMovieRequest request)
    {
        var movie = MovieEntity.Create(request.Title, request.Year, request.AgeRestriction, request.Price);
        await _container.CreateItemAsync(movie, new PartitionKey(movie.Id.ToString()));

        return movie;
    }

    public Task<MovieEntity> UpdateAsync(UpdateMovieRequest request, Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<MovieEntity>> GetMoviesAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<MovieEntity> GetMovieAsync(Guid id)
    {
        var query = $"SELECT * FROM c WHERE c.Id = '{id}'";

        Console.WriteLine($"Running query: {query}");

        var queryDefinition = new QueryDefinition(query);
        using var queryResultSetIterator = _container.GetItemQueryIterator<MovieEntity>(queryDefinition);

        MovieEntity result = default;

        while (queryResultSetIterator.HasMoreResults)
        {
            var currentResultSet = await queryResultSetIterator.ReadNextAsync();

            var count = currentResultSet.Count;

            if (count != 1)
            {
                return null;
            }

            result = currentResultSet.First();
        }

        return result;
    }

    public async Task CreateDataBaseAsync()
    {
        await _cosmosClient.CreateDatabaseIfNotExistsAsync(_databaseConfiguration.DatabaseId);
    }

    public async Task CreateContainerAsync()
    {
        const string partitionKey = "/Id";
        await _database.CreateContainerIfNotExistsAsync(_databaseConfiguration.ContainerId, partitionKey);
    }
}