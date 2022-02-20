First of all, I wish to propose the my source code inherited from the inital task but using "Clean Architecture"  with .net core development latest tendencies (especially its important when we plan the long-term product development cycles). .

What I propose (and you can find this in code).

**1. Domains Identifcation and BC (Bounded Contexts) definition:**
Ortogonal components of:
- **3 services:** 1. Users Service. 2. Accounts Service 3. Finance Ledger Service (In this example i've used double-entry strategy).  
- **1 Processor** - Payment Processor (Abstract). Using classic FSM (Finite State Machine for its complication)

![](https://i.stack.imgur.com/IAXY4.gif)

- **Services**: Anemic model & follow SRP
- **Payment processor:** represents the very basic SAGA transactional pattern within a single Persistence (sqlite in my example) aka SQL Transactions (usually in complex system we need to use the distributed transactions services like https://www.opensleigh.net/ , https://masstransit-project.com/ or https://axoniq.io/, AWS Step Functions, etc) - depends on complexity of project).

**2. CQRS / Mediator Pattern**
We initially should lay down a mechanism for processing large amount of data, which is not uncommon for microservice architectures. So i've divided API calls into the Commands and Queries. So its simple to keep data for read in denormalized way (mongodb, elasticache, etc)**

![](https://referbruv.com/data/Admin/2020/6/mediator-block.png)

**3. Persistence**
I've used UoW repository pattern, its simple and flexible and decouples business code from data Access. As a result, the persistence Framework can be changed without a great effort.

**4. Validation**
FluentValidation + Automapper + Error handling unification - usually most known tools in .net. I've implemented the basic approach.

**5. Task-Based Asynchonous services**
This is a pretty standard modern software API development requirement.

**6. Open API + Swagger for testing + xUnit test Fixtures**


**Top-3 readings / books / authors which influences most last 3 years:**
1. Chris Evans / DDD - https://www.amazon.com/Domain-Driven-Design-Tackling-Complexity-Software/dp/0321125215
2. Robert C Marting / Clean Architecture - https://www.amazon.com/Clean-Architecture-Craftsmans-Software-Structure/dp/0134494164
3. https://docs.microsoft.com/en-us/dotnet/architecture/microservices/  + https://www.amazon.com/Building-Event-Driven-Microservices-Leveraging-Organizational/dp/1492057894

*Please note - the provided sc is VERY basic according to what "please fund my account" usually means, and represents only general architecture approach which allows us to split the codebase into microservices in future and keep the control over code quality. So, if there any specific scope to test - please let me know.
