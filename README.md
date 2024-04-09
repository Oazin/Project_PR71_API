# UTGram API
API for the Angular web application 

## Table of Contents

- [Installation](#installation)
- [Configuration](#configuration)
- [Migration](#migration)
- [Run](#run)
- [Models](#models)

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
    "DefaultConnection": "Server=localhost; port=5432; user id = postgres; password = 0000; database=utgram; pooling=true; includeErrorDetail=true;"
}
```
## Migration
If you don't have database migration you can skip this part



## Run

## Models


- `GET /endpoint`: Description of what this endpoint does.
- `POST /endpoint`: Description of what this endpoint does.
- ...
