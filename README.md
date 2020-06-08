# Hexagonal Poc

The PoC presents Hexagonal Architecture to produce software implementation agnostic to technology, framework, or database. 

The result is focused on decoupling the technical code from the business logic and create reusable, testable code.

Based on a typical draw of a microservice in hexagonal architecture https://miro.medium.com/max/1400/1*sGcNVYsZmjxgp6LwQPCKKw.jpeg 
in this PoC, adapters are the `Providers`, ports are the `Services` and the microservice consisted from `Students.Domain` as the domain and `Students.Service` as the application.

`Students.Data` contains the entities and a custom implementation, that works, for `IDbContext`. 
`SqlDb` and `InMemoryDb` are 2 types of databases the providers are using to implement domain interfaces for providers.

To work with `SqlDb` you need to run `recreateDb.ps1` to create the School database in (localdb)\MSSQLLocalDB 
(if you don't have permissions to run powershell scripts just copy paste the contents of script to a powershell)

It's worth to mention that `Students.Service.Tests` is testing only the business of domain. The `Students.Provider.Tests` is testing only the infrastructure regardless the provider used.

There are lots of articles regarding hexagonal architecture on web, here is one to start https://beyondxscratch.com/2017/08/19/decoupling-your-technical-code-from-your-business-logic-with-the-hexagonal-architecture-hexarch/
