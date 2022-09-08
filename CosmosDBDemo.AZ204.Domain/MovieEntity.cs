using System;

namespace CosmosDBDemo.AZ204.Domain;

public class MovieEntity
{
    public Guid Id { get; }
    public string Title { get; private set; }
    public int Year { get; private set; }
    public int AgeRestriction { get; private set; }
    public float Price { get; private set; }

    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; private set; }

    private MovieEntity(string title, int year, int ageRestriction, float price)
    {
        Id = Guid.NewGuid();
        Title = title;
        Year = year;
        AgeRestriction = ageRestriction;
        Price = price;
        CreatedAt = DateTime.UtcNow;

        new MovieEntityValidator().ValidateAndThrowException(this);
    }

    private MovieEntity()
    {
    }

    public static MovieEntity Create(string title, int year, int ageRestriction, float price)
    {
        var movie = new MovieEntity(title, year, ageRestriction, price);

        return movie;
    }

    public void Update(string title, int year, int ageRestriction, float price)
    {
        Title = title;
        Year = year;
        AgeRestriction = ageRestriction;
        Price = price;
        UpdatedAt = DateTime.UtcNow;

        new MovieEntityValidator().ValidateAndThrowException(this);
    }
}