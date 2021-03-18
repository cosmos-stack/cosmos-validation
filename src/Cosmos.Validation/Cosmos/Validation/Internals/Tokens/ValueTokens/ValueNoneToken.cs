﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cosmos.Validation.Internals.Tokens.ValueTokens.Basic;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    /// <summary>
    /// None token
    /// </summary>
    internal class ValueNoneToken : ValueCollBasicToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "ValueNoneToken";

        /// <inheritdoc />
        public ValueNoneToken(VerifiableMemberContract contract, Func<object, bool> func) : base(contract, func, NAME) { }

        /// <summary>
        /// Impl of valid ops.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        protected override bool IsValidImpl(ICollection collection, Func<object, bool> func)
        {
            return !collection.Cast<object>().Any(one => func!.Invoke(one));
        }
    }

    /// <summary>
    /// None token, a generic version.
    /// </summary>
    /// <typeparam name="TVal"></typeparam>
    /// <typeparam name="TItem"></typeparam>
    internal class ValueNoneToken<TVal, TItem> : ValueCollBasicToken<TVal, TItem>
        where TVal : IEnumerable<TItem>
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "GenericValueNoneToken";

        /// <inheritdoc />
        public ValueNoneToken(VerifiableMemberContract contract, Func<TItem, bool> func) : base(contract, func, NAME) { }

        /// <summary>
        /// Impl of valid ops
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        protected override bool IsValidImpl(ICollection collection, Func<TItem, bool> func)
        {
            return !collection.Cast<TItem>().Any(one => func!.Invoke(one));
        }
    }
}