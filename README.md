# Base-API-Structure
.NET Core API Project Structure

## Libraries/Packages Used

### Entity Framework
These are used to allow us to work with a database using .NET objects. Removes the need for us to write any database access code. 
* Entity Framework Core - https://www.nuget.org/packages/Microsoft.EntityFrameworkCore
* Entity Framework Core Design - https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Design
* Entity Framework Core SQL Server - https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer

### AutoMapper
This is used to remove the need to write mapping code from one object to another. Used here to easily map our Database objects to DTOs from/to the client
* Automapper/Dependency Injection - https://www.nuget.org/packages/AutoMapper.Extensions.Microsoft.DependencyInjection/

### Patching
Required to have Patch operations available in the API
* JsonPatch - https://www.nuget.org/packages/Microsoft.AspNetCore.JsonPatch/

### General
Json Serialisation
* NewtonSoftJson - https://www.nuget.org/packages/Microsoft.AspNetCore.Mvc.NewtonsoftJson


## Some Steps

* Run the Entity Framework migration command to generate the database table creations based on your models.
  * dotnet ef migrations add *MigrationDescriptionName*
