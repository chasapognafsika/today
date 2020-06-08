# Description 

Contains sample for <strong>CRUD</strong> operations through <strong>ASP.NET Core Web API</strong> implementing <strong>Hexagonal Architecture</strong>.

You can test CRUD operations by using any API Testing Tool like <strong>Postman</strong>.

# Hexagonal Architecture

The PoC presents Hexagonal Architecture to produce software implementation agnostic to technology, framework, or database. 

The result is focused on decoupling the <strong>technical</strong> code from the <strong>business logic</strong> and create <strong>reusable</strong>, <strong>testable</strong> code.

Based on a typical draw of a microservice in hexagonal architecture https://miro.medium.com/max/1400/1*sGcNVYsZmjxgp6LwQPCKKw.jpeg 
in this PoC, <strong>adapters</strong> are the <strong>`Providers`</strong>, <strong>ports</strong> are the <strong>`Services`</strong> and the microservice consisted from <strong>`Clients.Domain`</strong> as the domain and <strong>`Clients.Service`</strong> as the application.

<strong>`Clients.Data`</strong> contains the entities and a custom implementation, that works, for `IDbContext`. 
<strong>`SqlDb`</strong> and <strong>`InMemoryDb`</strong> are 2 types of databases the providers are using to implement domain interfaces for providers.

To work with <strong>`SqlDb`</strong> you need to run `recreateDb.ps1` to create the Clients database in (localdb)\MSSQLLocalDB 
(if you don't have permissions to run powershell scripts just copy paste the contents of script to a powershell)

It's worth to mention that <strong>`Clients.Service.Tests`</strong> is testing only the <strong>business</strong> of domain. The <strong>`Clients.Provider.Tests`</strong> is testing only the <strong>infrastructure</strong> regardless the provider used.

There are lots of articles regarding hexagonal architecture on web, like https://beyondxscratch.com/2017/08/19/decoupling-your-technical-code-from-your-business-logic-with-the-hexagonal-architecture-hexarch/
