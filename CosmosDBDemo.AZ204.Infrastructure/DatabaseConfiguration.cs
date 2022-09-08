namespace CosmosDBDemo.AZ204.Infrastructure;

public class DatabaseConfiguration
{
    public string EndpointUrl { get; set; }
    public string PrimaryKey { get; set; }
    public string DatabaseId { get; set; }
    public string ContainerId { get; set; }
    public string PartitionKey { get; set; }
}