# .Net6 N-Tier API Project

The project has been created in a layered architecture model with the .Net 6 library.

## Layers;

`Core` > `Repository` > `Service` > `Api`

MSSQL Server was used as database.

## Duties of layers:

`CORE LAYER:`
In the core layer, the structures we need in the center are used, such as the entity model we will use, the interfaces required for the warehouse and service, and the Dto objects responsible for carrying data.

`REPOSITORY LAYER:`
In the repository layer, the details of the properties of the objects are given. The necessary operations for the database have been completed. Necessary configurations have been made.

`SERVICE LAYER:`
Since the service layer is now the layer that will carry data to our API layer, it is the layer where the business logics are. In this layer, validation operations required for the data to be sent to the repository layer are written. Structures to be converted between objects and data transfer objects (dto) have been established.

`API LAYER:`
The API layer is the layer that will interact with the end user. That's why all configs and filters are generated in this layer.

## Used Libraries

`Entity Framework Core`
`Entity Framework Core Sql Server`
`Entity Framework Core Tools`
`AutoMapper`
`FluentValidation`
`Swashbuckle (Swagger)`
`Entity Framework Core Design`
