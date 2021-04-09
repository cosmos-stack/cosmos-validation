﻿namespace Cosmos.Validation.Internals
{
    internal class InternalValidationHandlerFactory : IValidationHandlerFactory
    {
        public ValidationHandlerBuilder CreateBuilder() => ValidationHandlerBuilder.Create();
    }
}