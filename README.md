First of all, I want to mention that the provided source code architecture doesnt follow  the modern flexible development tendencies especially when we plan the long-term product development cycles.  Do can find my solution which you can percieve as a refactoring of initial sc based on my vision of sofware architecture within complex domain projects.

What I propose (and you can find this in code).

**1. Domains Identifcation and BC (Bounded Contexts) definition
-- 3 services 1. Users Service. 2. Accounts Service 3. Finance Ledger Service (In this example i've used double-entry strategy) and 
-- 1 Processor - Payment Processor (Abstract).

Services: Anemic model & follow SRP, 
Payment processor: represents the very basic SAGA transactional pattern within a single Persistence (sqlite in my example) aka SQL Transactions (usually in complex system we need to use the distributed transactions services like https://www.opensleigh.net/ , tps://masstransit-project.com/ or https://axoniq.io/, AWS Step Functions, etc) - depends on complexity of project).

**2. CQRS / Mediator Pattern
We initially should lay down a mechanism for processing large amount of data, which is not uncommon for microservice architectures. So i've divided API calls into the Commands and Queries. So its simple to keep data for read in denormalized way (mongodb, elasticache, etc)

4. Persistence
I've used UoW pattern, its simple and flexible and decouples business code from data Access. As a result, the persistence Framework can be changed without a great effort.


4. Validation
 FluentValidation + Automapper + Error handling unification - usually most known tools in .net. I've implemented the basic approach.

5. Task-Based Asynchonous services 
This is a standard modern software development.

6. Open API + Swagger for testing

Top-3 readings / books / authors which influences most last 3 years:
1. Chris Evans / DDD - https://www.amazon.com/Domain-Driven-Design-Tackling-Complexity-Software/dp/0321125215
2. Robert C Marting / Clean Architecture - https://www.amazon.com/Clean-Architecture-Craftsmans-Software-Structure/dp/0134494164
3. https://docs.microsoft.com/en-us/dotnet/architecture/microservices/  + https://www.amazon.com/Building-Event-Driven-Microservices-Leveraging-Organizational/dp/1492057894

Please note - the provided sc is VERY basic according to what "please fund my account" usually means, and represents only general architecture approach which allows to split the codebase into microservices in future and control the code quality. So, if there any specific scope to test - please let me know.
