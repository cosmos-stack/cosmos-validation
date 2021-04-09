using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cosmos.Validation.Internals.Tokens.ValueTokens.Basic;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    /// <summary>
    /// Any token
    /// </summary>
    internal class ValueAnyToken : ValueCollBasicToken
    {
        private const string Name = "ValueAnyToken";

        /// <inheritdoc />
        public ValueAnyToken(VerifiableMemberContract contract, Func<object, bool> func) : base(contract, func, Name) { }

        /// <summary>
        /// Impl of valid ops.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        protected override bool IsValidImpl(ICollection collection, Func<object, bool> func)
        {
            return collection.Cast<object>().Any(one => func!.Invoke(one));
        }
    }

    /// <summary>
    /// Any token, a generic version.
    /// </summary>
    /// <typeparam name="TVal"></typeparam>
    /// <typeparam name="TItem"></typeparam>
    internal class ValueAnyToken<TVal, TItem> : ValueCollBasicToken<TVal, TItem>
        where TVal : IEnumerable<TItem>
    {
        private const string Name = "GenericValueAnyToken";

        /// <inheritdoc />
        public ValueAnyToken(VerifiableMemberContract contract, Func<TItem, bool> func) : base(contract, func, Name) { }

        /// <summary>
        /// Impl of valid ops
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        protected override bool IsValidImpl(ICollection collection, Func<TItem, bool> func)
        {
            return collection.Cast<TItem>().Any(one => func!.Invoke(one));
        }
    }
}