using System;
using System.Threading.Tasks;
using CosmosDBDemo.AZ204.DTO;
using CosmosDBDemo.AZ204.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace CosmosDBDemo.AZ204.Controllers;

[ApiController]
[Route("[controller]")]
public class CosmosDbDemoController : ControllerBase
{
    private readonly IMoviesDataContext _moviesDataContext;

    public CosmosDbDemoController(IMoviesDataContext moviesDataContext)
    {
        _moviesDataContext = moviesDataContext;
    }

    [HttpPost]
    public async Task<IActionResult> InsertMovie([FromBody] CreateMovieRequest request)
    {
        var result = await _moviesDataContext.InsertAsync(request);

        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetMovie([FromRoute] Guid id)
    {
        var result = await _moviesDataContext.GetMovieAsync(id);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetMovies()
    {
        var result = await _moviesDataContext.GetMoviesAsync();

        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateMovie([FromRoute] Guid id, [FromBody] UpdateMovieRequest request)
    {
        var result = await _moviesDataContext.UpdateAsync(request, id);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteMovie([FromRoute] Guid id)
    {
        var result = await _moviesDataContext.DeleteAsync(id);

        return Ok(result);
    }
}