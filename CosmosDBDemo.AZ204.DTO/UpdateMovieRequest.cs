namespace CosmosDBDemo.AZ204.DTO;

public record UpdateMovieRequest(string Title, int Year, int AgeRestriction, float Price);