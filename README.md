# Security:

-	net core
-	JWT, Jwt bearer
-	SSO – Single sign-on
-	OIDC - Open ID Connect
-	Auth, OAuth 2.0
-	Two way authentication ?
-	Federated security ?
-	RSA, SHA 256, private public key ...
- Cookies

# remove all below after cleanum of repo

## Gold Backend 

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




# GoldFrontend

http://goldprices.azurewebsites.net/

Angular application hosted on .NET Core for displaying daily gold prices chart. It is a client to Gold Service Rest Api prividing daily gold prices. 

http://goldandcurrencies.azurewebsites.net

Angular application hosted on .NET Core based on a free model. Planned to be used to display variuos financial data, currencies exchange rates, gold prices in various currencies.  

## Getting Started - how to run on localhost

1. have backend running: https://github.com/mjoniec/GoldBackend/edit/master/README.md
2. Open this project in VS and run


## Prerequisites

What you need to install before run the software

- .NET Core
- node.js and npm
- jqx
- "#" can not be in local path else angular app will not launch


## Installing

Node.js, npm
https://nodejs.org/en/download/

https://www.jqwidgets.com/angular-components-documentation/documentation/create-jqwidgets-angular-app/index.htm

jqx
npm install -g create-jqwidgets-angular-app
https://www.jqwidgets.com/jquery-widgets-documentation/documentation/package-managers/npm-tutorial.htm


## Deployment
Deployed on azure

Angular SPA
http://goldprices.azurewebsites.net/

REST API
http://goldprices.azurewebsites.net/api/GoldData/GoldDaily

Static resource
http://goldprices.azurewebsites.net/GoldPricesExampleFrontendBackup.json

