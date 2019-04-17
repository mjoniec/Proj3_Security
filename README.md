# Gold Backend

Services and libraries that obtain gold prices daily data from external service, (de)serializes, changes and exposes it through API. 

## Getting Started - how to run on localhost

1. C:\...Mqtt.Service
2. dotnet run --MQTT:Port=1883
3. C:\...GoldExternalApiClient.Service
4. dotnet run --GoldExternalApiClient:Name="Gold data service"
5. run Gold.Service web api project from VS - it will popup browser with 1 call result - request data id
6. postman send get request with request data id obtained from step 5 - https://localhost:44350/api/Gold/GetAll/47934 - response body should contain gold prices per date array

## Prerequisites

- .NET Core


## Running the tests

VS standard


## Deployment

To be deployed on azure


## Authors

* **Marcin Joniec** - *All contributions*


