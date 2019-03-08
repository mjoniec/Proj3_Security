# GoldBackend

*Gold.Service* - service to provide (processed) gold data for frontend client
- Core Rest Api
- Swagger

--------------------------------------------------------

*Data.Model* - (de)serializable Model
- attributes for JSON 
- RDB properties 

*Data.Repositories* - Access to DB and example (generated) data 
- EntityFramework
- LINQ

*Data.Services* - Logic between controllers and repositories
- IOC
- Autofac
- JSON / XML parsing logic

*Data.Services.Test* - unit test project
- xUnit
- NSubstitude

--------------------------------------------------------

*Mqtt.Service* - Logic between controllers and repositories
- .net core general host custom application type
- mqtt protocol client only nuget dependent (no 3rd party software installation on host)

*Mqtt.Commonlib* - stores client to communicate between components
- sync and async versions

--------------------------------------------------------

ExternalGoldDataApiClient.Service
- .net core general host custom application type
- sends http request to external service with gold info and receives JSON response
- client of internal mqtt service
