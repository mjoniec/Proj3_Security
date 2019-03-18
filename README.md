# GoldBackend

1. C:\C#\GoldBackend\Mqtt.Service
2. dotnet run --MQTT:Port=1883
3. C:\C#\GoldBackend\GoldExternalApiClient.Service
4. dotnet run --GoldExternalApiClient:Name="Gold data service"
5. run Gold.Service web api
6. postman send get request - triggers services creation
7. postman send post request - should contain gold data

--------------------------------------------------------

*Gold.Service* 
- service to provide gold price data for frontend client
- Core Rest Api
- Double data request strategy, first accepted, then return if created
- IOC, DI
- Autofac
- Swagger (TODO)

--------------------------------------------------------

*Data.Model*
- (de)serializable Model
- attributes for JSON
- attributes for DB (TODO)

*Data.Repositories*
- Access to DB (TODO)
- LINQ
- Core Entity Framework (TODO)

*Data.Services*
- Logic between controllers and any data access
- IOC, DI
- JSON parsing logic
- Mqtt client

*Data.Services.Test*
- unit test project for services
- xUnit
- NSubstitude

--------------------------------------------------------

*Mqtt.Service*
- mqtt protocol service privider
- Service - core General Host application type
- only nuget dependent (no 3rd party software installation on host)

*Mqtt.Client* 
- sends and receives messages between components
- publis and subscribe as a listener on separate topics (channels) 
- fully async

--------------------------------------------------------

ExternalGoldDataApiClient.Service
- sends http request to external service, receives JSON response
- Service - core General Host application type
- client of external API service
- JSON parsing logic, reverse engineered from 3rd pary (to be moved from service)
