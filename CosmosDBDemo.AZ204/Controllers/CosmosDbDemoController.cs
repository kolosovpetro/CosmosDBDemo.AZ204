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
    public async Task<IActionResult> InsertMovie(CreateMovieRequest request)
    {
        var result = await _moviesDataContext.InsertAsync(request);

        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }
}