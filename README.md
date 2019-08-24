# Gold Backend

Services and libraries that obtain gold prices daily data from external service, (de)serializes, changes and exposes it through API. 

## Getting Started - how to run on localhost

1. C:\...Mqtt.Service
2. dotnet run --MQTT:Port=1883
3. C:\...GoldExternalApiClient.Service
4. dotnet run --GoldExternalApiClient:Name="Gold data service"
5. run Gold.Service web api project from VS - 
6. if not executed by default run in browser or postman 
https://localhost:44350/swagger/index.html
https://localhost:44350/Gold - returns the status of the service

7. https://localhost:44350/Gold/GetDataPrepared - it will return accepted status with requestId and start collecting data
requestId: 48720

8. use requestId to send second get request https://localhost:44350/Gold/GetData/48720 - response body should contain daily gold prices array - pairs of date and value

## Prerequisites

- .NET Core


## Running the tests

VS standard


## Deployment

To be deployed on azure


## Authors

* **Marcin Joniec** - *All contributions*


