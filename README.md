# Insurance Services (Microservices .NET)

Insurance sales system made in a Microservice architecture using:

#### Technology Stack
.NET 8, Entity Framework Core, MediatR, Marten, Eureka, Ocelot, JWT Tokens, RestEase, RawRabbit, NHibernate, Polly, NEST (ElasticSearch client), Dapper, DynamicExpresso, SignalR.

#### Business Case
We are going to build very simplified system for insurance agents to sell various kind of insurance products. Insurance agents will have to log in and system will present them with list of products they can sell. Agents will be able to view products and find a product appropriate for their customers. Then they can create an offer and system will calculate a price based on provided parameters.
Finally agent will be able to confirm the sale by converting offer to policy and printing pdf certificate.
Portal will also give them ability to search and view offer and policies.
Portal will also have some basic social network features like chat for agents.
Latest feature is a business dashboard that displays sales stats using ElasticSearch Aggregations and ChartJS.

#### Architecture overview
.NET Microservices Architecture:

![UML Diagram](https://github.com/user-attachments/assets/9efff0cf-576c-4567-811b-3fac143e1d7b)

**Web** 
VueJS Single Page Application that provides insurance agents ability to select appropriate product for their customers, calculate price, create an offer and conclude the sales process by converting offer to policy. This application also provides search and view functions for policies and offers. Frontend talks to backend services via agent-portal-gateway.

**Agent Portal API Gateway** 
Special microservice whose main purpose is to hide complexity of the underlying back office services structure from client application. API gateway provides also security barrier and does not allow unauthenticated request to be passed to backend services. Another popular usage of API gateways is content aggregation from multiple services.

**Auth Service**
Service responsible for users authentication. Our security system will be based on JWT tokens. Once user identifies himself correctly, auth service issues a token that is further use to check user permission and available products.

**Chat Service**
Service that uses SignalR to give agents ability to chat with each other.

**Payment Service**
Main responsibilities: create Policy Account, show Policy Account list, register in payments from bank statement file.
This module is taking care of a managing policy accounts. Once the policy is created, an account is created in this service with expected money income. PaymentService also has an implementation of a scheduled process where CSV file with payments is imported and payments are assigned to policy accounts. This component shows asynchronous communication between services using RabbitMQ and ability to create background jobs. It also features accessing database using Dapper.

**Policy Service**
Creates offers, converts offers to insurance policies.
In this service we demonstrated usage of CQRS pattern for better read/write operation isolation. This service demonstrates two ways of communication between services: synchronous REST based calls to PricingService through HTTP Client to get the price, and asynchronous event based using RabbitMQ to publish information about newly created policies. In this service we also access RDBMS using NHibernate.

**Policy Search Service**
Provides insurance policy search.
This module listens for events from RabbitMQ, converts received DTOs to “read model” and indexes given model in ElasticSearch to provide advanced search capabilities.

**Pricing Service** 
Service responsible for calculation of price for given insurance product based on its parametrization.
For each product a tariff should be defined. The tariff is a set of rules on the basis of which the price is calculated. DynamicExpresso was used to parse the rules. During the policy purchase process, the PolicyService connects with this service to calculate a price. Price is calculated based on user’s answers for defined questions.

**Product Service**
Insurance product catalog.
It provides basic information about each insurance product and its parameters that can be customized while creating an offer for a customer.

**Document Service**
Service uses JS Report to generate pdf certificates.

**Dashboard Service**
Dashboard that presents sales statistics.
Business dashboards that presents our agents sales results. Dashboard service subscribes to events of selling policies and index sales data in ElasticSearch. Then ElasticSearch aggregation framework is used to calculate sales stats like: total sales and number of policies per product per time period, sales per agent in given time period and sales timeline. Sales stats are nicely visualized using ChartJS.

Each business microservice has also .Api project (PaymentService.Api, PolicyService.Api etc.), where we defined commands, events, queries and operations and .Test project (PaymentService.Test, PolicyService.Test) with unit and integration tests.

## Running with Docker

You must install Docker & Docker Compose before. \
Scripts have been divided into two parts:

- [`infra.yml`](scripts/infra.yml) runs the necessary infrastructure.
- [`app.yml`](scripts/app.yml) is used to run the application.

You can use scripts to build/run/stop/down all containers.

To run the whole solution:

```bash
cd scripts
./infra-run.sh
./app-run.sh
```

> If ElasticSearch fails to start, try running `sudo sysctl -w vm.max_map_count=262144` first

Once the application and infrastructure are started you can open http://localhost:8080 in your browser and see our welcome page.
Once there you can use Account menu item to log into the system. You can for example login as admin with password admin.

## Manual running

### Prerequisites

Install [PostgreSQL](https://www.postgresql.org/) version >= 10.0.

Install [RabbitMQ](https://www.rabbitmq.com/).

Install [Elasticsearch](https://www.elastic.co/guide/en/elasticsearch/reference/current/install-elasticsearch.html) version >= 6.

### Init databases

#### Windows

```bash
cd postgres
"PATH_TO_PSQL.EXE" --host "localhost" --port EXAMPLE_PORT --username "EXAMPLE_USER" --file "createdatabases.sql"
```

In my case this command looks like:

```bash
cd postgres
"C:\Program Files\PostgreSQL\9.6\bin\psql.exe" --host "localhost" --port 5432 --username "postgres" --file "createdatabases.sql"
```

#### Linux

```bash
sudo -i -u postgres
psql --host "localhost" --port 5432 --username "postgres" --file "PATH_TO_FILE/createdatabases.sql"
```

This script should create `lab_user` user and the following databases: `lab_netmicro_payments`, `lab_netmicro_jobs`, `lab_netmicro_policy` and `lab_netmicro_pricing`.

### Run Eureka

Service registry and discovery tool for our project is Eureka. It is included in the project.
In order to start it open terminal / command prompt.

```bash
cd eureka-server
./gradlew.[bat] bootRun
```

This should start Eureka and you should be able to go to http://localhost:8761/ and see Eureka management panel.

### Build

Build all projects from command line without test:

#### Windows

```bash
cd scripts
build-without-tests.bat
```

#### Linux

```bash
cd scripts
./build-without-tests.sh
```

Build all projects from command with test:

#### Windows

```bash
cd scripts
build.bat
```

#### Linux

```bash
cd scripts
./build.sh
```

## Run specific service

Go to folder with specific service (`PolicyService`, `ProductService` etc) and use `dotnet run` command.
