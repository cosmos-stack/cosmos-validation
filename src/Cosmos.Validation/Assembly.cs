using System.Runtime.CompilerServices;

//Cosmos Validation UnitTests
[assembly: InternalsVisibleTo("CosmosValidationUT")]

//Cosmos Validation Sinks
[assembly: InternalsVisibleTo("Cosmos.Validation.Sinks.DataAnnotations")]
[assembly: InternalsVisibleTo("Cosmos.Validation.Sinks.FluentValidation")]

//Cosmos Prowess Package
[assembly: InternalsVisibleTo("Cosmos.Extensions.ObjectVisitors")]