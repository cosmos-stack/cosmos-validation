using System;
using System.Collections.Generic;
using System.Linq;
using Cosmos.Validation.Annotations;
using Cosmos.Validation.Internals;
using Cosmos.Validation.Internals.Extensions;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Validators
{
    public class ChinaIdNumberValidator : CustomValidator<string>
    {
        private ValidationOptions _options { get; set; }

        public ChinaIdNumberValidator() : base("China IdCard Number Validator")
        {
            _options = new ValidationOptions();
        }

        private ChinaIdNumberValidator(ValidationOptions options) : base("China IdCard Number Validator")
        {
            _options = options ?? new ValidationOptions();
        }

        public static ChinaIdNumberValidator Instance { get; } = new();

        #region Verify By String

        public VerifyResult Verify(string idNumber, ChinaIdNumberValidationOptions options)
        {
            var result = new ChinaIdNumberInfo();
            var isNumberSpan = new ReadOnlySpan<char>(idNumber.ToArray());
            List<VerifyFailure> failures = new();

            if (!Assists.ValidLength(isNumberSpan, options, failures, result))
                return new VerifyResult(failures);

            if (!Assists.ValidBirthday(isNumberSpan, options, failures, result))
                return new VerifyResult(failures);

            if (!Assists.ValidArea(isNumberSpan, options, failures, result))
                return new VerifyResult(failures);

            if (!Assists.ValidCheckBit(isNumberSpan, options, failures, result))
                return new VerifyResult(failures);

            if (!Assists.ValidTheRest(isNumberSpan, options, failures, result))
                return new VerifyResult(failures);

            return VerifyResult.Success;
        }

        public VerifyResult Verify(string idNumber, Action<ChinaIdNumberValidationOptions> optionsAct)
        {
            var options = new ChinaIdNumberValidationOptions();
            optionsAct?.Invoke(options);
            return Verify(idNumber, options);
        }

        public VerifyResult Verify(string idNumber, ChinaIdStyles styles = ChinaIdStyles.Id18, int minYear = 0, ChinaIdAreaValidLimit limit = ChinaIdAreaValidLimit.Province, bool ignoreCheckBit = false)
        {
            return Verify(idNumber, new ChinaIdNumberValidationOptions(styles) { MinYear = minYear, Limit = limit, IgnoreCheckBit = ignoreCheckBit });
        }

        public VerifyResult Verify(string idNumber, string paramName, ChinaIdStyles styles = ChinaIdStyles.Id18, int minYear = 0, ChinaIdAreaValidLimit limit = ChinaIdAreaValidLimit.Province, bool ignoreCheckBit = false)
        {
            return Verify(idNumber, new ChinaIdNumberValidationOptions(styles, paramName) { MinYear = minYear, Limit = limit, IgnoreCheckBit = ignoreCheckBit });
        }

        #endregion

        #region (Impl for CustomValidator) Verify

        private VerifyResult VerifyImpl(VerifiableObjectContext context, ChinaIdNumberValidationOptions options)
        {
            List<VerifyResult> results = new();
            var values = context.GetValuesWithAttribute<ChinaIdNumberAttribute>();

            foreach (var value in values)
            {
                // 如果 Value 为 String，对其进行验证
                if (value.Value is string idNumber)
                {
                    var attr = value.GetAttributes<ChinaIdNumberAttribute>().FirstOrDefault();

                    if (attr is null)
                        continue;

                    results.Add(Verify(idNumber, options));
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
            var values = context.GetValuesWithAttribute<ChinaIdNumberAttribute>();

            foreach (var value in values)
            {
                // 如果 Value 为 String，对其进行验证
                if (value.Value is string emailValue)
                {
                    var attr = value.GetAttributes<ChinaIdNumberAttribute>().FirstOrDefault();

                    if (attr is null)
                        continue;

                    var options = new ChinaIdNumberValidationOptions(attr.Style, value.MemberName)
                    {
                        MinYear = attr.MinYear,
                        Limit = attr.Limit,
                        IgnoreCheckBit = attr.IgnoreCheckBit
                    };

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

        public VerifyResult Verify(Type type, object instance, ChinaIdNumberValidationOptions options)
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

        public VerifyResult Verify(Type type, object instance, Action<ChinaIdNumberValidationOptions> optionsAct)
        {
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            var options = new ChinaIdNumberValidationOptions();
            optionsAct?.Invoke(options);

            if (instance is null)
                return _options.ReturnNullReferenceOrSuccess(options.ParamName);

            return Verify(type, instance, options);
        }

        public VerifyResult Verify<T>(T instance, ChinaIdNumberValidationOptions options)
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

        public VerifyResult Verify<T>(T instance, Action<ChinaIdNumberValidationOptions> optionsAct)
        {
            var options = new ChinaIdNumberValidationOptions();
            optionsAct?.Invoke(options);

            if (instance is null)
                return _options.ReturnNullReferenceOrSuccess(options.ParamName);

            return Verify(instance, options);
        }

        #endregion

        #region (Impl for CustomValidator) VerifyOne

        private VerifyResult VerifyOneImpl(VerifiableMemberContext context, ChinaIdNumberValidationOptions options)
        {
            if (context is null)
                return _options.ReturnNullReferenceOrSuccess();

            // 如果 Value 为 String，对其进行验证
            if (context.Value is string idNumber)
            {
                var attr = context.GetAttributes<ChinaIdNumberAttribute>().FirstOrDefault();

                if (attr is null)
                    return VerifyResult.NullReferenceWith("There's no ChinaIdNumberAttribute on this Member.");

                return Verify(idNumber, options);
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
                var attr = context.GetAttributes<ChinaIdNumberAttribute>().FirstOrDefault();

                if (attr is null)
                    return VerifyResult.NullReferenceWith("There's no ChinaIdNumberAttribute on this Member.");

                var options = new ChinaIdNumberValidationOptions(attr.Style, context.MemberName)
                {
                    MinYear = attr.MinYear,
                    Limit = attr.Limit,
                    IgnoreCheckBit = attr.IgnoreCheckBit
                };

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
    }
}