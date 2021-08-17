using System.Runtime.CompilerServices;

//Cosmos Validation UnitTests
[assembly: InternalsVisibleTo("CosmosValidationUT")]

//Cosmos Validation Inline library
[assembly: InternalsVisibleTo("Cosmos.Validation.Annotations")]
[assembly: InternalsVisibleTo("Cosmos.Validation.Dependency")]

//Cosmos Validation Extensions
[assembly: InternalsVisibleTo("Cosmos.Validation.Extensions.Email")]
[assembly: InternalsVisibleTo("Cosmos.Validation.Extensions.Encryption")]
[assembly: InternalsVisibleTo("Cosmos.Validation.Extensions.Verification")]
[assembly: InternalsVisibleTo("Cosmos.Validation.Extensions.ChinaIdNumber")]

//Cosmos Validation Sinks
[assembly: InternalsVisibleTo("Cosmos.Validation.Sinks.DataAnnotations")]
[assembly: InternalsVisibleTo("Cosmos.Validation.Sinks.FluentValidation")]
[assembly: InternalsVisibleTo("Cosmos.Validation.Sinks.SwallowValidation")]

//Cosmos Prowess Package
[assembly: InternalsVisibleTo("Cosmos.Extensions.ObjectVisitors")]