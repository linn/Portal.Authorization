# Portal Authorization

## Project Summary
Application for managing authorization permissions and associations related to accessing internal APIs for users of the public facing portal app.

Provides APIs for creating new users (to service the cognito PostConfirmation lambda), associating users to retailers and managing permissions regarding those users access to resources pertaining to retailers they are associated with.

Also provides a react client for GUI management of the above.

## Component technologies
* The backend is dotnet core C# with minimal third party dependencies.
* Service.Host uses the .NET minimal API web framework
* The javascript client app is built with React and managed with npm and vite.
* Javacript tests are run with vitest. React components are testing using [React Testing Library](https://testing-library.com/docs/react-testing-library/intro/)
* Local debugging of the client should be performed using node v22 for best results.
* Persistence is to a postgres database via EF Core.
* Continuous deployment via Docker container to AWS ECS using Travis CI.

## Local running and Testing
### C# service
* Restore nuget packages.
* Run C# tests as preferred.
* run or debug the Service.Host project to start the backend.

### Client
* `npm i` to install npm packages.
* `npm start` to run client locally on port 3000.
* `npm test` to run javascript tests.

### Routes
With the current configuration, all requests to 
* app.linn.co.uk/portal-authorization
should be handled by this app
