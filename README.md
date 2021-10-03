# Security - net core with JWT

Demo API for accessing protected endpoints with token based authosisation and authentication for users. Screenshots in documentation.

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
