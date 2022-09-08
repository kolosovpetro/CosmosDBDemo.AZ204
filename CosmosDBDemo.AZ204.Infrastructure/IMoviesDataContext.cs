using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CosmosDBDemo.AZ204.Domain;
using CosmosDBDemo.AZ204.DTO;

namespace CosmosDBDemo.AZ204.Infrastructure;

public interface IMoviesDataContext
{
    Task<MovieEntity> InsertAsync(CreateMovieRequest request);
    Task<MovieEntity> UpdateAsync(UpdateMovieRequest request, Guid id);
    Task<bool> DeleteAsync(Guid id);
    Task<List<MovieEntity>> GetMoviesAsync();
    Task<MovieEntity> GetMovieAsync(Guid id);
    Task CreateDataBaseAsync();
    Task CreateContainerAsync();
}