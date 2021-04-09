using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cosmos.Validation.Internals.Tokens.ValueTokens.Basic;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    /// <summary>
    /// All token
    /// </summary>
    internal class ValueAllToken : ValueCollBasicToken
    {
        private const string Name = "ValueAllToken";

        /// <inheritdoc />
        public ValueAllToken(VerifiableMemberContract contract, Func<object, bool> func) : base(contract, func, Name) { }

        /// <summary>
        /// Impl of valid ops.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        protected override bool IsValidImpl(ICollection collection, Func<object, bool> func)
        {
            return collection.Cast<object>().All(one => func!.Invoke(one));
        }
    }

    /// <summary>
    /// All token, a generic version.
    /// </summary>
    /// <typeparam name="TVal"></typeparam>
    /// <typeparam name="TItem"></typeparam>
    internal class ValueAllToken<TVal, TItem> : ValueCollBasicToken<TVal, TItem>
        where TVal : IEnumerable<TItem>
    {
        private const string Name = "GenericValueAllToken";

        /// <inheritdoc />
        public ValueAllToken(VerifiableMemberContract contract, Func<TItem, bool> func) : base(contract, func, Name) { }

        /// <summary>
        /// Impl of valid ops
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        protected override bool IsValidImpl(ICollection collection, Func<TItem, bool> func)
        {
            return collection.Cast<TItem>().All(one => func!.Invoke(one));
        }
    }
}