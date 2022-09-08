using System;
using Newtonsoft.Json;

namespace CosmosDBDemo.AZ204.Domain;

public class MovieEntity
{
    [JsonProperty(PropertyName = "id")] public Guid Id { get; set; }
    [JsonProperty(PropertyName = "title")] public string Title { get; set; }
    [JsonProperty(PropertyName = "year")] public int Year { get; set; }

    [JsonProperty(PropertyName = "ageRestriction")]
    public int AgeRestriction { get; set; }

    [JsonProperty(PropertyName = "price")] public float Price { get; set; }

    [JsonProperty(PropertyName = "createdAt")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty(PropertyName = "updatedAt")]
    public DateTime? UpdatedAt { get; set; }

    public MovieEntity(string title, int year, int ageRestriction, float price)
    {
        Id = Guid.NewGuid();
        Title = title;
        Year = year;
        AgeRestriction = ageRestriction;
        Price = price;
        CreatedAt = DateTime.UtcNow;

        new MovieEntityValidator().ValidateAndThrowException(this);
    }

    public MovieEntity()
    {
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