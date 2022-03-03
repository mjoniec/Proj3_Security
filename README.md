Testing: ASP Net Core 6, Security, Middleware, Async, DI, Google Search Api

# HttpsTestApi

Asp enforcing https redirecion to all requests with hsts

# JWTApi

Demo API for accessing protected endpoints with token based authosisation and authentication for users. Screenshots in documentation https://github.com/mjoniec/Security/wiki

User management controller, no authorisation
- list users https://localhost:44370/user
- create a new user, always sets a normal Role https://localhost:44370/user/Create
- login with the user, valid for 5 minutes - returns token https://localhost:44370/user/Login

Protected data controller, Token beared authorisation

- requires authorisation with bearer token - any user any role https://localhost:44370/protected/GetProtectedDataForAnyUser
- requires authorisation with bearer token - user with normal or admin role https://localhost:44370/protected/GetProtectedDataForAdminOrNormalUser
- requires authorisation with bearer token - only admin https://localhost:44370/protected/GetProtectedDataForAdmin

based on this tutorial 
- https://medium.com/@evandro.ggomes/json-web-token-authentication-with-asp-net-core-2-0-b074b0cfc870

# LoginCookieApp

Testing build in authorisation and authentication middleware with cookie scheme

# MiddlewareTest

Api with middleware pipeline branch on condition

# SearchApi

Usage of google search api library, wrapped with custom asp 6 api. Separate pipeline and services classes. 