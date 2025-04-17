# MeterRead

## Projects

### MeterRead.Api

A minimal API project which contains the main endpoint for uploading meter readings.

### MeterRead.Api.Tests

Unit tests for the API project. The tests are written using xUnit and Moq.

### MeterRead.Application

- **MeterDataParserService** : Parses the CSV data and outputs usable models
- **MeterDataValidatorService** : Validates the parsed data
- **MeterDataService** : Contains import logic and interacts with the database context

### MeterRead.Data

The data project contains the database context and entity models for the application. It uses Entity Framework Core to interact with the database.

I have used Sqlite for this project

- **Database Context**: Defines the database context for the application
- **Entity Models**: Defines the entity models for the application
- Includes a basic seed script 'Seed.sql' to populate the database with initial data.