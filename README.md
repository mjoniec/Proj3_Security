# HOW TO RUN on localhost

1. C:\C#\GoldBackend\Mqtt.Service
2. dotnet run --MQTT:Port=1883
3. C:\C#\GoldBackend\GoldExternalApiClient.Service
4. dotnet run --GoldExternalApiClient:Name="Gold data service"
5. run Gold.Service web api
6. postman send get request - triggers services creation
7. postman send post request - should contain gold data
