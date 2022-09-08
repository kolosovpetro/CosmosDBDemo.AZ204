using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CosmosDBDemo.AZ204.Domain;
using CosmosDBDemo.AZ204.DTO;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace CosmosDBDemo.AZ204.Infrastructure;

public class MoviesDataContext : IMoviesDataContext
{
    private readonly Container _container;

    public MoviesDataContext(Container container)
    {
        _container = container;
    }

    public async Task<MovieEntity> InsertAsync(CreateMovieRequest request)
    {
        var movie = new MovieEntity(request.Title, request.Year, request.AgeRestriction, request.Price);
        await _container.CreateItemAsync(movie, new PartitionKey(movie.Id.ToString()));

        return movie;
    }

    public async Task<MovieEntity> UpdateAsync(UpdateMovieRequest request, Guid id)
    {
        var movieId = id.ToString();
        var partitionKey = new PartitionKey(movieId);
        var movie = await _container.ReadItemAsync<MovieEntity>(movieId, partitionKey);

        var itemBody = movie.Resource;

        if (itemBody == null)
        {
            return null;
        }

        itemBody.Update(request.Title, request.Year, request.AgeRestriction, request.Price);

        await _container.ReplaceItemAsync(
            itemBody,
            itemBody.Id.ToString(),
            new PartitionKey(itemBody.Id.ToString()));

        return itemBody;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var movieId = id.ToString();
        var partitionKey = new PartitionKey(movieId);

        try
        {
            await _container.DeleteItemAsync<MovieEntity>(movieId, partitionKey);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }

    public async Task<List<MovieEntity>> GetMoviesAsync()
    {
        const string query = @"SELECT * FROM items";

        var iterator = _container.GetItemQueryIterator<MovieEntity>(query);

        var matches = new List<MovieEntity>();

        while (iterator.HasMoreResults)
        {
            var next = await iterator.ReadNextAsync();
            matches.AddRange(next);
        }

        return matches;
    }

    public async Task<MovieEntity> GetMovieAsync(Guid id)
    {
        var iterator = _container.GetItemLinqQueryable<MovieEntity>()
            .Where(m => m.Id == id)
            .ToFeedIterator();

        var matches = new List<MovieEntity>();

        while (iterator.HasMoreResults)
        {
            var next = await iterator.ReadNextAsync();
            matches.AddRange(next);
        }

        return matches.SingleOrDefault();
    }
}