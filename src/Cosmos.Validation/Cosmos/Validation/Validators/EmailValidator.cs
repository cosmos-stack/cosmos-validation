using System;
using System.Collections.Generic;
using System.Linq;
using Cosmos.Validation.Annotations;
using Cosmos.Validation.Internals.Extensions;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Validators
{
    public class EmailValidationOptions
    {
        public EmailValidationOptions() { }

        public EmailValidationOptions(bool allowTopLevelDomains, bool allowInternational)
        {
            AllowTopLevelDomains = allowTopLevelDomains;
            AllowInternational = allowInternational;
        }

        public EmailValidationOptions(bool allowTopLevelDomains, bool allowInternational, string paramName)
        {
            AllowTopLevelDomains = allowTopLevelDomains;
            AllowInternational = allowInternational;
            ParamName = paramName;
        }

        public bool AllowTopLevelDomains { get; set; }
        public bool AllowInternational { get; set; }

        internal string ParamName { get; set; } = "Instance";

        internal static EmailValidationOptions Default { get; } = new();
    }

    public class EmailValidator : CustomValidator<string>
    {
        private ValidationOptions _options { get; set; }

        public EmailValidator() : base("Email Validator")
        {
            _options = new ValidationOptions();
        }

        private EmailValidator(ValidationOptions options) : base("Email Validator")
        {
            _options = options ?? new ValidationOptions();
        }

        public static EmailValidator Instance { get; } = new();

        private const string AtomCharacters = "!#$%&'*+-/=?^_`{|}~";

        #region Verify By String

        public VerifyResult Verify(string instance, EmailValidationOptions options)
        {
            var index = 0;
            List<VerifyFailure> failures = new();

            if (options is null)
                options = EmailValidationOptions.Default;

            if (!ShouldNotBeNull(instance, options, failures))
                return new VerifyResult(failures);

            if (!ShouldNotBeTooShortOrLong(instance, options, failures))
                return new VerifyResult(failures);

            if (instance[index] == '"')
            {
                if (!ShouldSkipQuoted(instance, ref index, options, failures))
                    return new VerifyResult(failures);
            }
            else
            {
                if (!ShouldSkipAtom(instance, ref index, options, failures))
                    return new VerifyResult(failures);

                if (!ShouldSkipAtomInLoop(instance, ref index, options, failures))
                    return new VerifyResult(failures);
            }

            if (!ShouldContainAtSymbol(instance, ref index, options, failures))
                return new VerifyResult(failures);

            if (instance[index] != '[')
            {
                if (!ShouldSkipDomain(instance, ref index, options, failures))
                    return new VerifyResult(failures);

                if (!ShouldEqualsBetweenLengthAndIndex(instance, index, options, failures))
                    return new VerifyResult(failures);

                return VerifyResult.Success;
            }

            // address literal
            index++;

            if (!ShouldAtLeastEightOrMoreCharacters(instance, index, options, failures))
                return new VerifyResult(failures);

            if (!MayIpAddress(instance, ref index, options, failures))
                return new VerifyResult(failures);

            if (!ShouldSkipDomainEnd(instance, ref index, options, failures))
                return new VerifyResult(failures);

            if (!ShouldEqualsBetweenLengthAndIndex(instance, index, options, failures))
                return new VerifyResult(failures);

            return VerifyResult.Success;
        }

        public VerifyResult Verify(string instance, Action<EmailValidationOptions> optionsAct)
        {
            var options = new EmailValidationOptions();
            optionsAct?.Invoke(options);
            return Verify(instance, options);
        }

        public VerifyResult Verify(string instance, bool allowTopLevelDomains = false, bool allowInternational = false)
        {
            return Verify(instance, new EmailValidationOptions(allowTopLevelDomains, allowInternational));
        }

        public VerifyResult Verify(string instance, string paramName, bool allowTopLevelDomains, bool allowInternational)
        {
            return Verify(instance, new EmailValidationOptions(allowTopLevelDomains, allowInternational, paramName));
        }

        #endregion

        #region (Impl for CustomValidator) Verify

        private VerifyResult VerifyImpl(VerifiableObjectContext context, EmailValidationOptions options)
        {
            List<VerifyResult> results = new();
            var values = context.GetValuesWithAttribute<ValidEmailValueAttribute>();

            foreach (var value in values)
            {
                // 如果 Value 为 String，对其进行验证
                if (value.Value is string emailValue)
                {
                    var attr = value.GetAttributes<ValidEmailValueAttribute>().FirstOrDefault();

                    if (attr is null)
                        continue;

                    results.Add(Verify(emailValue, options));
                }
                // 否则，如果 Value 不是基础类型（即 Value 为引用类型、结构等），对其进一步解析并验证
                else if (!value.BasicTypeState())
                {
                    results.Add(VerifyImpl(value.ConvertToObjectContext(), options));
                }
                // 否则，认为其类型不是所期待的
                else
                {
                    results.Add(VerifyResult.UnexpectedTypeWith(value.MemberName));
                }
            }

            return VerifyResult.MakeTogether(results);
        }

        protected override VerifyResult VerifyImpl(VerifiableObjectContext context)
        {
            if (context is null)
                return _options.ReturnNullReferenceOrSuccess();

            List<VerifyResult> results = new();
            var values = context.GetValuesWithAttribute<ValidEmailValueAttribute>();

            foreach (var value in values)
            {
                // 如果 Value 为 String，对其进行验证
                if (value.Value is string emailValue)
                {
                    var attr = value.GetAttributes<ValidEmailValueAttribute>().FirstOrDefault();

                    if (attr is null)
                        continue;

                    var options = new EmailValidationOptions(attr.AllowTopLevelDomains, attr.AllowInternational, value.MemberName);

                    results.Add(Verify(emailValue, options));
                }
                // 否则，如果 Value 不是基础类型（即 Value 为引用类型、结构等），对其进一步解析并验证
                else if (!value.BasicTypeState())
                {
                    results.Add(VerifyImpl(value.ConvertToObjectContext()));
                }
                // // 否则，认为其类型不是所期待的
                // else
                // {
                //     results.Add(VerifyResult.UnexpectedTypeWith(value.MemberName));
                // }
            }

            return VerifyResult.MakeTogether(results);
        }

        public VerifyResult Verify(Type type, object instance, EmailValidationOptions options)
        {
            if (type is null)
                throw new ArgumentNullException(nameof(type));
            if (instance is null)
                return _options.ReturnNullReferenceOrSuccess(options?.ParamName ?? "Instance");
            if (instance is string str)
                return Verify(str, options);
            if (instance is VerifiableObjectContext objectContext)
                return VerifyImpl(objectContext, options);
            if (instance is VerifiableMemberContext memberContext)
                return VerifyOneImpl(memberContext, options);
            objectContext = _objectResolver.Resolve(type, instance);
            return VerifyImpl(objectContext, options);
        }

        public VerifyResult Verify(Type type, object instance, Action<EmailValidationOptions> optionsAct)
        {
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            var options = new EmailValidationOptions();
            optionsAct?.Invoke(options);

            if (instance is null)
                return _options.ReturnNullReferenceOrSuccess(options.ParamName);

            return Verify(type, instance, options);
        }

        public VerifyResult Verify<T>(T instance, EmailValidationOptions options)
        {
            if (instance is null)
                return _options.ReturnNullReferenceOrSuccess(options?.ParamName ?? "Instance");
            if (instance is string str)
                return Verify(str, options);
            if (instance is VerifiableObjectContext objectContext)
                return VerifyImpl(objectContext, options);
            if (instance is VerifiableMemberContext memberContext)
                return VerifyOneImpl(memberContext, options);
            objectContext = _objectResolver.Resolve(instance);
            return VerifyImpl(objectContext, options);
        }

        public VerifyResult Verify<T>(T instance, Action<EmailValidationOptions> optionsAct)
        {
            var options = new EmailValidationOptions();
            optionsAct?.Invoke(options);

            if (instance is null)
                return _options.ReturnNullReferenceOrSuccess(options.ParamName);

            return Verify(instance, options);
        }

        #endregion

        #region (Impl for CustomValidator) VerifyOne

        private VerifyResult VerifyOneImpl(VerifiableMemberContext context, EmailValidationOptions options)
        {
            if (context is null)
                return _options.ReturnNullReferenceOrSuccess();

            // 如果 Value 为 String，对其进行验证
            if (context.Value is string emailValue)
            {
                var attr = context.GetAttributes<ValidEmailValueAttribute>().FirstOrDefault();

                if (attr is null)
                    return VerifyResult.NullReferenceWith("There's no ValidEmailValueAttribute on this Member.");

                return Verify(emailValue, options);
            }

            // 否则，如果 Value 不是基础类型（即 Value 为引用类型、结构等），对其进一步解析并验证
            if (!context.BasicTypeState())
            {
                return VerifyImpl(context.ConvertToObjectContext());
            }

            // 否则，认为其类型不是所期待的
            return VerifyResult.UnexpectedTypeWith(context.MemberName);
        }

        protected override VerifyResult VerifyOneImpl(VerifiableMemberContext context)
        {
            if (context is null)
                return _options.ReturnNullReferenceOrSuccess();

            // 如果 Value 为 String，对其进行验证
            if (context.Value is string emailValue)
            {
                var attr = context.GetAttributes<ValidEmailValueAttribute>().FirstOrDefault();

                if (attr is null)
                    return VerifyResult.NullReferenceWith("There's no ValidEmailValueAttribute on this Member.");

                var options = new EmailValidationOptions(attr.AllowTopLevelDomains, attr.AllowInternational, context.MemberName);

                return Verify(emailValue, options);
            }

            // 否则，如果 Value 不是基础类型（即 Value 为引用类型、结构等），对其进一步解析并验证
            if (!context.BasicTypeState())
            {
                return VerifyImpl(context.ConvertToObjectContext());
            }

            // 否则，认为其类型不是所期待的
            return VerifyResult.UnexpectedTypeWith(context.MemberName);
        }

        #endregion

        #region Internal Methods Impl

        private static bool ShouldNotBeNull(string instance, EmailValidationOptions options, List<VerifyFailure> failures)
        {
            if (instance is not null)
                return true;

            failures.Add(new(options.ParamName, "Instance cannot be null."));

            return false;
        }

        private static bool ShouldNotBeTooShortOrLong(string instance, EmailValidationOptions options, List<VerifyFailure> failures)
        {
            if (instance.Length > 0 && instance.Length <= 256)
                return true;

            failures.Add(new(options.ParamName, "The email address is too long or too short."));

            return false;
        }

        private static bool ShouldSkipQuoted(string instance, ref int index, EmailValidationOptions options, List<VerifyFailure> failures)
        {
            if (!Helpers.SkipQuoted(instance, ref index, options.AllowInternational) || index >= instance.Length)
            {
                failures.Add(new(options.ParamName, "The length of the email address is illegal."));
                return false;
            }

            return true;
        }

        private static bool ShouldSkipAtom(string instance, ref int index, EmailValidationOptions options, List<VerifyFailure> failures)
        {
            if (!Helpers.SkipAtom(instance, ref index, options.AllowInternational) || index >= instance.Length)
            {
                failures.Add(new(options.ParamName, "The length of the email address is illegal."));
                return false;
            }

            return true;
        }

        private static bool ShouldSkipAtomInLoop(string instance, ref int index, EmailValidationOptions options, List<VerifyFailure> failures)
        {
            while (instance[index] == '.')
            {
                index++;

                if (index >= instance.Length)
                {
                    failures.Add(new(options.ParamName, "The email address is not valid."));
                    return false;
                }

                if (!Helpers.SkipAtom(instance, ref index, options.AllowInternational))
                {
                    failures.Add(new(options.ParamName, "The email address is not valid."));
                    return false;
                }

                if (index >= instance.Length)
                {
                    failures.Add(new(options.ParamName, "The email address is not valid."));
                    return false;
                }
            }

            return true;
        }

        private static bool ShouldContainAtSymbol(string instance, ref int index, EmailValidationOptions options, List<VerifyFailure> failures)
        {
            if (index + 1 >= instance.Length || index >= 64 || instance[index++] != '@')
            {
                failures.Add(new(options.ParamName, "The email address does not contain the @ symbol."));

                return false;
            }

            return true;
        }

        private static bool ShouldSkipDomain(string instance, ref int index, EmailValidationOptions options, List<VerifyFailure> failures)
        {
            if (!Helpers.SkipDomain(instance, ref index, options.AllowTopLevelDomains, options.AllowInternational))
            {
                failures.Add(new(options.ParamName, "The domain in the email address is invalid."));

                return false;
            }

            return true;
        }

        private static bool ShouldAtLeastEightOrMoreCharacters(string instance, int index, EmailValidationOptions options, List<VerifyFailure> failures)
        {
            if (index + 8 >= instance.Length)
            {
                failures.Add(new(options.ParamName, "The length of the email address is abnormal."));

                return false;
            }

            return true;
        }

        private static bool MayIpAddress(string instance, ref int index, EmailValidationOptions options, List<VerifyFailure> failures)
        {
            var ipv6 = instance.Substring(index, 5);
            if (ipv6.ToLowerInvariant() == "ipv6:")
            {
                index += "IPv6:".Length;
                if (!Helpers.SkipIPv6Literal(instance, ref index))
                {
                    failures.Add(new(options.ParamName, "The email address is invalid."));
                    return false;
                }
            }
            else
            {
                if (!Helpers.SkipIPv4Literal(instance, ref index))
                {
                    failures.Add(new(options.ParamName, "The email address is invalid."));
                    return false;
                }
            }

            return true;
        }

        private static bool ShouldSkipDomainEnd(string instance, ref int index, EmailValidationOptions options, List<VerifyFailure> failures)
        {
            if (index >= instance.Length || instance[index++] != ']')
            {
                failures.Add(new(options.ParamName, "The email address is invalid."));

                return false;
            }

            return true;
        }

        private static bool ShouldEqualsBetweenLengthAndIndex(string instance, int index, EmailValidationOptions options, List<VerifyFailure> failures)
        {
            if (index == instance.Length)
                return true;

            failures.Add(new(options.ParamName, "The email address is invalid."));

            return false;
        }

        #endregion

        #region Internal Enum

        /// <summary>
        /// Sub domain type
        /// </summary>
        [Flags]
        private enum SubDomainType
        {
            None = 0,
            Alphabetic = 1,
            Numeric = 2,
            AlphaNumeric = 3
        }

        #endregion

        #region Internal Helpers

        /// <summary>
        /// Internal helpers
        /// </summary>
        private static class Helpers
        {
            public static bool IsDigit(char c)
            {
                return (c >= '0' && c <= '9');
            }

            public static bool IsLetter(char c)
            {
                return (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z');
            }

            public static bool IsLetterOrDigit(char c)
            {
                return IsLetter(c) || IsDigit(c);
            }

            public static bool IsAtom(char c, bool allowInternational)
            {
                return c < 128 ? IsLetterOrDigit(c) || AtomCharacters.IndexOf(c) != -1 : allowInternational;
            }

            public static bool IsDomain(char c, bool allowInternational, ref SubDomainType type)
            {
                if (c < 128)
                {
                    if (IsLetter(c) || c == '-')
                    {
                        type |= SubDomainType.Alphabetic;
                        return true;
                    }

                    if (IsDigit(c))
                    {
                        type |= SubDomainType.Numeric;
                        return true;
                    }

                    return false;
                }

                if (allowInternational)
                {
                    type |= SubDomainType.Alphabetic;
                    return true;
                }

                return false;
            }

            public static bool IsDomainStart(char c, bool allowInternational, out SubDomainType type)
            {
                if (c < 128)
                {
                    if (IsLetter(c))
                    {
                        type = SubDomainType.Alphabetic;
                        return true;
                    }

                    if (IsDigit(c))
                    {
                        type = SubDomainType.Numeric;
                        return true;
                    }

                    type = SubDomainType.None;

                    return false;
                }

                if (allowInternational)
                {
                    type = SubDomainType.Alphabetic;
                    return true;
                }

                type = SubDomainType.None;

                return false;
            }

            public static bool SkipAtom(string text, ref int index, bool allowInternational)
            {
                int startIndex = index;

                while (index < text.Length && IsAtom(text[index], allowInternational))
                    index++;

                return index > startIndex;
            }

            public static bool SkipSubDomain(string text, ref int index, bool allowInternational, out SubDomainType type)
            {
                int startIndex = index;

                if (!IsDomainStart(text[index], allowInternational, out type))
                    return false;

                index++;

                while (index < text.Length && IsDomain(text[index], allowInternational, ref type))
                    index++;

                // Don't allow single-character top-level domains.
                if (index == text.Length && (index - startIndex) == 1)
                    return false;

                return (index - startIndex) <= 64 && text[index - 1] != '-';
            }

            public static bool SkipDomain(string text, ref int index, bool allowTopLevelDomains, bool allowInternational)
            {
                SubDomainType type;

                if (!SkipSubDomain(text, ref index, allowInternational, out type))
                    return false;

                if (index < text.Length && text[index] == '.')
                {
                    do
                    {
                        index++;

                        if (index == text.Length)
                            return false;

                        if (!SkipSubDomain(text, ref index, allowInternational, out type))
                            return false;
                    } while (index < text.Length && text[index] == '.');
                }
                else if (!allowTopLevelDomains)
                {
                    return false;
                }

                // Note: by allowing AlphaNumeric, we get away with not having to support punycode.
                if (type == SubDomainType.Numeric)
                    return false;

                return true;
            }

            public static bool SkipQuoted(string text, ref int index, bool allowInternational)
            {
                bool escaped = false;

                // skip over leading '"'
                index++;

                while (index < text.Length)
                {
                    if (text[index] >= 128 && !allowInternational)
                        return false;

                    if (text[index] == '\\')
                    {
                        escaped = !escaped;
                    }
                    else if (!escaped)
                    {
                        if (text[index] == '"')
                            break;
                    }
                    else
                    {
                        escaped = false;
                    }

                    index++;
                }

                if (index >= text.Length || text[index] != '"')
                    return false;

                index++;

                return true;
            }

            public static bool SkipIPv4Literal(string text, ref int index)
            {
                int groups = 0;

                while (index < text.Length && groups < 4)
                {
                    int startIndex = index;
                    int value = 0;

                    while (index < text.Length && text[index] >= '0' && text[index] <= '9')
                    {
                        value = (value * 10) + (text[index] - '0');
                        index++;
                    }

                    if (index == startIndex || index - startIndex > 3 || value > 255)
                        return false;

                    groups++;

                    if (groups < 4 && index < text.Length && text[index] == '.')
                        index++;
                }

                return groups == 4;
            }

            public static bool IsHexDigit(char c)
            {
                return (c >= 'A' && c <= 'F') || (c >= 'a' && c <= 'f') || (c >= '0' && c <= '9');
            }

            // This needs to handle the following forms:
            //
            // IPv6-addr = IPv6-full / IPv6-comp / IPv6v4-full / IPv6v4-comp
            // IPv6-hex  = 1*4HEXDIG
            // IPv6-full = IPv6-hex 7(":" IPv6-hex)
            // IPv6-comp = [IPv6-hex *5(":" IPv6-hex)] "::" [IPv6-hex *5(":" IPv6-hex)]
            //             ; The "::" represents at least 2 16-bit groups of zeros
            //             ; No more than 6 groups in addition to the "::" may be
            //             ; present
            // IPv6v4-full = IPv6-hex 5(":" IPv6-hex) ":" IPv4-address-literal
            // IPv6v4-comp = [IPv6-hex *3(":" IPv6-hex)] "::"
            //               [IPv6-hex *3(":" IPv6-hex) ":"] IPv4-address-literal
            //             ; The "::" represents at least 2 16-bit groups of zeros
            //             ; No more than 4 groups in addition to the "::" and
            //             ; IPv4-address-literal may be present
            public static bool SkipIPv6Literal(string text, ref int index)
            {
                bool compact = false;
                int colons = 0;

                while (index < text.Length)
                {
                    int startIndex = index;

                    while (index < text.Length && IsHexDigit(text[index]))
                        index++;

                    if (index >= text.Length)
                        break;

                    if (index > startIndex && colons > 2 && text[index] == '.')
                    {
                        // IPv6v4
                        index = startIndex;

                        if (!SkipIPv4Literal(text, ref index))
                            return false;

                        return compact ? colons < 6 : colons == 6;
                    }

                    int count = index - startIndex;
                    if (count > 4)
                        return false;

                    if (text[index] != ':')
                        break;

                    startIndex = index;
                    while (index < text.Length && text[index] == ':')
                        index++;

                    count = index - startIndex;
                    if (count > 2)
                        return false;

                    if (count == 2)
                    {
                        if (compact)
                            return false;

                        compact = true;
                        colons += 2;
                    }
                    else
                    {
                        colons++;
                    }
                }

                if (colons < 2)
                    return false;

                return compact ? colons < 7 : colons == 7;
            }
        }

        #endregion
    }
}