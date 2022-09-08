# Azure Cosmos DB Demo

[![Run Build and Test](https://github.com/kolosovpetro/CosmosDBDemo.AZ204/actions/workflows/run-build-and-test-dotnet.yml/badge.svg)](https://github.com/kolosovpetro/CosmosDBDemo.AZ204/actions/workflows/run-build-and-test-dotnet.yml)

Yet again another demo in scope of my AZ204 exam preparation.
This time Azure Cosmos Db simple CRUD including key concepts of implementation.

## Infrastructure provisioning

- Create resource group: `az group create --name "azure-cosmos-demo-rg" --location "westus"`
- Create cosmos db account: `az cosmosdb create --name "cosmosdbpkolosov" --resource-group "azure-cosmos-demo-rg"`
- Create database: 
  - `az cosmosdb database create --name "cosmosdbpkolosov" --resource-group "azure-cosmos-demo-rg" --db-name "MoviesDatabase"` (legacy)
  - `az cosmosdb sql database create --account-name "cosmosdbpkolosov" --resource-group "azure-cosmos-demo-rg" --name "MoviesDatabase"`
- Create container (table): `az cosmosdb sql container create -g "azure-cosmos-demo-rg" -a "cosmosdbpkolosov" -d "MoviesDatabase" -n "Movies" --partition-key-path "/id"`
- Drop resource group: `az group delete --name "azure-cosmos-demo-rg"`
- Print all subscriptions: `az account list --output table`

## Packages

- `dotnet add package FluentValidation --version 11.2.1`
- `dotnet add package Microsoft.Azure.Cosmos --version 3.30.1`