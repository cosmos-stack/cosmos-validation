using System.Runtime.CompilerServices;

//Cosmos Validation UnitTests
[assembly: InternalsVisibleTo("CosmosValidationUT")]

//Cosmos Validation Inline library
[assembly: InternalsVisibleTo("CosmosStack.Validation.Annotations")]
[assembly: InternalsVisibleTo("CosmosStack.Validation.Dependency")]

//Cosmos Validation Extensions
[assembly: InternalsVisibleTo("CosmosStack.Validation.Extensions.Email")]
[assembly: InternalsVisibleTo("CosmosStack.Validation.Extensions.Encryption")]
[assembly: InternalsVisibleTo("CosmosStack.Validation.Extensions.Verification")]
[assembly: InternalsVisibleTo("CosmosStack.Validation.Extensions.ChinaIdNumber")]

//Cosmos Validation Sinks
[assembly: InternalsVisibleTo("CosmosStack.Validation.Sinks.DataAnnotations")]
[assembly: InternalsVisibleTo("CosmosStack.Validation.Sinks.FluentValidation")]

//Cosmos Prowess Package
[assembly: InternalsVisibleTo("CosmosStack.Extensions.ObjectVisitors")]