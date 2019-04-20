# How to start service
## Requirements
- Docker with Linux containers support
## Start service
1. Run `docker-compose up web`
2. Service will be available on [http://localhost:8080](http://localhost:8080)
## Swagger
Swagger available using next [link](http://localhost:8080/swagger)
## Alternative way to start service
1. Install .NET Core SDK 2.1
2. Run `dotnet run --urls http://*:8080 --no-launch-profile` in folder BackendFinal.Api
# How to start unit tests
## Requirements
- Docker with Linux containers support
## Start unit tests
1. Run `docker-compose up unit`
## Alternative way to start unit tests
1. Install .NET Core SDK 2.1
2. Run `dotnet test` in folder BackendFinal.UnitTests
# How to start integration tests
## Requirements
- Docker with Linux containers support
## Start unit tests
1. Run `docker-compose up integration`
## Alternative way to start unit tests
1. Install .NET Core SDK 2.1
2. Run `dotnet test` in folder BackendFinal.IntegrationTests
# Notes
SCV parsed while service started only once. <br />
To update data first update CSV file and after recreate DB. <br />
Data adds to DB during single transaction. <br />
In case if data is incorrect whole data won't be added to DB. <br />
CSV parsing occurs only when table dbo.PriceModels is empty. <br />
SCV parsing is long process (wait about 5 mins). <br />