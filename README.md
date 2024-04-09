# UTGram API
API for the Angular web application 

## Table of Contents

- [Installation](#installation)
- [Configuration](#configuration)
- [Migration](#migration)
- [Run](#run)
- [Swagger](#swagger)

## Installation

Prerequisites

Before running this API, ensure that you have the following installed:

- [NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [PostgreSQL](https://www.postgresql.org/download/)

## Configuration 
Before running the API, you need to configure the PostgreSQL connection string in the appsettings.json file located in the project root. Update the ConnectionStrings section with your PostgreSQL connection details.

_Exemple_
``` json
"ConnectionStrings": {
    "DefaultConnection": "Server=localhost; port=5432; user id = postgres; password = YourPSQLPassword; database=utgram; pooling=true; includeErrorDetail=true;"
}
```
## Migration
If you don't have database migration you can skip this part. 

Else launch pgAdmin connect you to the server _PostgreSQL ~16~_ with the password that you defined at the installation of PostgreSQL. <br>
Then, create a new database with the name _utgram_ (Rigth-Click on Databases -> Create). <br>
After, Rigth-Click on utgram -> Restore. On the new opened window select the utgram database dump on your computer. <br>
Finally click on Restore button. 

## Run
Start powershell 
``` powershell
    > cd ./path/to/api/file/Project_PR71_API/Project_PR71_API
    > dotnet build
    > dotnet run
```

## Swagger 
This API use Swagger to test each endpoint. 
To access : [http://localhost:5242/swagger/index.html](http://localhost:5242/swagger/index.html)

