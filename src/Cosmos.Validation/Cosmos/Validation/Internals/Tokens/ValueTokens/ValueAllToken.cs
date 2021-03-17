using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cosmos.Validation.Internals.Tokens.ValueTokens.Basic;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    internal class ValueAllToken : ValueCollBasicToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "ValueAllToken";

        public ValueAllToken(VerifiableMemberContract contract, Func<object, bool> func) : base(contract, func, NAME) { }

        protected override bool IsValidImpl(ICollection collection, Func<object, bool> func)
        {
            return collection.Cast<object>().All(one => func!.Invoke(one));
        }
    }

    internal class ValueAllToken<TVal, TItem> : ValueCollBasicToken<TVal, TItem>
        where TVal : IEnumerable<TItem>
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "GenericValueAllToken";

        public ValueAllToken(VerifiableMemberContract contract, Func<TItem, bool> func) : base(contract, func, NAME) { }

        protected override bool IsValidImpl(ICollection collection, Func<TItem, bool> func)
        {
            return collection.Cast<TItem>().All(one => func!.Invoke(one));
        }
    }
}