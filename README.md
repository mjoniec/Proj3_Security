# GoldBackend

1 C:\C#\GoldBackend\Mqtt.Service

dotnet run --MQTT:Port=1883

2 C:\C#\GoldBackend\GoldExternalApiClient.Service

dotnet run --GoldExternalApiClient:Name="Gold data service"

3 run Gold.Service web api

4

postman send get request - triggers services creation
postman send post request - should contain gold data

--------------------------------------------------------

*Gold.Service* - service to provide (processed) gold data for frontend client
- Autofac
- Core Rest Api
- Swagger

--------------------------------------------------------

*Data.Model* - (de)serializable Model
- attributes for JSON 

*Data.Repositories* - Access to DB and example (generated) data 
- LINQ

*Data.Services* - Logic between controllers and repositories
- IOC
- JSON parsing logic
- Mqtt client

*Data.Services.Test* - unit test project
- xUnit
- NSubstitude

--------------------------------------------------------

*Mqtt.Service* - Logic between controllers and repositories
- .net core general host custom application type
- mqtt protocol client only nuget dependent (no 3rd party software installation on host)

*Mqtt.Client* - communicate between components
- publish messages to topic
- subscribe to topic, listener for incoming messages 

--------------------------------------------------------

ExternalGoldDataApiClient.Service
- .net core general host custom application type
- sends http request to external service with gold info and receives JSON response
- client of internal mqtt service
