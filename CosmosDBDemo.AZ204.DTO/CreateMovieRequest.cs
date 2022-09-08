namespace CosmosDBDemo.AZ204.DTO;

public record CreateMovieRequest(string Title, int Year, int AgeRestriction, float Price);