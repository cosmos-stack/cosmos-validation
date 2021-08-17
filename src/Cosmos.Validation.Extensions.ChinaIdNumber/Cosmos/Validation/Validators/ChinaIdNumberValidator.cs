using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Cosmos.Date;
using Cosmos.Text;
using Cosmos.Validation.Annotations;
using Cosmos.Validation.Internals.Extensions;
using Cosmos.Validation.Internals.Standards;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Validators
{
    public class ChinaIdNumberValidationOptions
    {
        public ChinaIdNumberValidationOptions() { }

        public ChinaIdNumberValidationOptions(ChinaIdLength length)
        {
            Length = length;
        }

        public ChinaIdNumberValidationOptions(ChinaIdLength length, string paramName)
        {
            Length = length;
            ParamName = paramName;
        }

        public ChinaIdLength Length { get; set; }

        public int MinYear { get; set; } = 0;

        public ChinaIdAreaValidLimit Limit { get; set; } = ChinaIdAreaValidLimit.Province;

        public bool IgnoreCheckBit { get; set; } = false;

        internal string ParamName { get; set; } = "Instance";

        internal static ChinaIdNumberValidationOptions Default { get; } = new();
    }

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

        private static readonly int[] WeightFactors = { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
        private static readonly char[] CheckBits = { '1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2' };

        #region Verify By String

        public VerifyResult Verify(string idNumber, ChinaIdNumberValidationOptions options)
        {
            var result = new ChinaIdNumberInfo();
            List<VerifyFailure> failures = new();

            if (!ValidLength(idNumber, options, failures, result))
                return new VerifyResult(failures);

            if (!ValidBirthday(idNumber, options, failures, result))
                return new VerifyResult(failures);

            if (!ValidArea(idNumber, options, failures, result))
                return new VerifyResult(failures);

            if (!ValidCheckBit(idNumber, options, failures, result))
                return new VerifyResult(failures);

            if (!ValidTheRest(idNumber, options, failures, result))
                return new VerifyResult(failures);

            return VerifyResult.Success;
        }

        public VerifyResult Verify(string idNumber, Action<ChinaIdNumberValidationOptions> optionsAct)
        {
            var options = new ChinaIdNumberValidationOptions();
            optionsAct?.Invoke(options);
            return Verify(idNumber, options);
        }

        public VerifyResult Verify(string idNumber, ChinaIdLength length = ChinaIdLength.Id18, int minYear = 0, ChinaIdAreaValidLimit limit = ChinaIdAreaValidLimit.Province, bool ignoreCheckBit = false)
        {
            return Verify(idNumber, new ChinaIdNumberValidationOptions(length) { MinYear = minYear, Limit = limit, IgnoreCheckBit = ignoreCheckBit });
        }

        public VerifyResult Verify(string idNumber, string paramName, ChinaIdLength length = ChinaIdLength.Id18, int minYear = 0, ChinaIdAreaValidLimit limit = ChinaIdAreaValidLimit.Province, bool ignoreCheckBit = false)
        {
            return Verify(idNumber, new ChinaIdNumberValidationOptions(length, paramName) { MinYear = minYear, Limit = limit, IgnoreCheckBit = ignoreCheckBit });
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

                    var options = new ChinaIdNumberValidationOptions(attr.Length, value.MemberName)
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

                var options = new ChinaIdNumberValidationOptions(attr.Length, context.MemberName)
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

        #region Internal Methods Impl

        internal static bool ValidLength(string idNumber, ChinaIdNumberValidationOptions options, List<VerifyFailure> failures, ChinaIdNumberInfo info)
        {
            if (string.IsNullOrWhiteSpace(idNumber))
            {
                failures.Add(new(options.ParamName, "Instance cannot be null."));
                return false;
            }

            if (options.Length == ChinaIdLength.Id15 && idNumber.Length != 15)
            {
                failures.Add(new(options.ParamName, "The length of the instance must be 15."));
                return false;
            }

            if (options.Length == ChinaIdLength.Id18 && idNumber.Length != 18)
            {
                failures.Add(new(options.ParamName, "The length of the instance must be 18."));
                return false;
            }

            info.Length = options.Length;
            return true;
        }

        internal static bool ValidBirthday(string idNumber, ChinaIdNumberValidationOptions options, List<VerifyFailure> failures, ChinaIdNumberInfo info)
        {
            DateTime birthday;
            var @try = options.Length switch
            {
                ChinaIdLength.Id15 => __getBirthday15(idNumber, out birthday),
                ChinaIdLength.Id18 => __getBirthday18(idNumber, out birthday),
                _ => throw new ArgumentOutOfRangeException()
            };

            if (!@try)
            {
                failures.Add(new(options.ParamName, "The date of birth cannot be recognized."));
                return false;
            }

            var now = DateTimeFactory.Now().Date;
            @try = birthday > DateTime.MinValue && birthday.Year >= options.MinYear && birthday <= now;
            info.Birthday = birthday;

            if (!@try)
            {
                failures.Add(new(options.ParamName, "The date of birth is invalid or exceeds the limit."));
                return false;
            }

            return true;

            // ReSharper disable once InconsistentNaming
            bool __getBirthday15(string number, out DateTime date)
            {
                var s = $"19{number.Substring(6, 6)}";
                return DateTime.TryParseExact(s, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date)
                    && s.All(c => c.IsNumber());
            }

            // ReSharper disable once InconsistentNaming
            bool __getBirthday18(string number, out DateTime date)
            {
                var s = number.Substring(6, 8);
                return DateTime.TryParseExact(s, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date)
                    && s.All(c => c.IsNumber());
            }
        }

        internal static bool ValidArea(string idNumber, ChinaIdNumberValidationOptions options, List<VerifyFailure> failures, ChinaIdNumberInfo info)
        {
            var s = idNumber.Substring(0, 6);
            if (!s.All(c => c.IsNumber()))
            {
                failures.Add(new(options.ParamName, "Invalid administrative area code."));
                return false;
            }

            var areaNumber = int.Parse(s);
            var areaInfo = __getDeepestArea(areaNumber);
            var @try = areaInfo is not null && areaInfo.GetDepth() >= (int)options.Limit;

            info.RecognizableArea = areaInfo;
            info.AreaNumber = areaNumber;

            if (!@try)
            {
                if (areaInfo is null)
                    failures.Add(new(options.ParamName, "Invalid administrative area code."));
                else
                    failures.Add(new VerifyFailure(options.ParamName, $"Administrative area's level is lower than level {options.Limit}"));
                return false;
            }

            return true;

            // ReSharper disable once InconsistentNaming
            ChinaIdAreaInfo __getDeepestArea(int iArea)
            {
                ChinaIdAreaInfo area = null, lastArea = null;
                var d = GBT2260_2013.Singleton.GetDictionary();
                while (iArea > 0)
                {
                    if (iArea < 10)
                        throw new ArgumentException("Administrative code is wrong.");
                    if (d.ContainsKey(iArea))
                    {
                        var t = new ChinaIdAreaInfo(iArea, d[iArea]);
                        if (area is null)
                            area = t;
                        else
                            lastArea.Parent = t;
                        lastArea = t;
                    }

                    iArea /= 100;
                }

                return area;
            }
        }

        internal static bool ValidCheckBit(string idNumber, ChinaIdNumberValidationOptions options, List<VerifyFailure> failures, ChinaIdNumberInfo info)
        {
            char checkBit;
            var @try = options.Length switch
            {
                ChinaIdLength.Id15 => __isRight15(idNumber, out checkBit),
                ChinaIdLength.Id18 => __isRight18(idNumber, out checkBit),
                _ => throw new ArgumentOutOfRangeException()
            } || options.IgnoreCheckBit;

            if (!@try)
            {
                failures.Add(new(options.ParamName, "Wrong check code."));
                return false;
            }

            info.CheckBit = checkBit;

            return true;

            // ReSharper disable once InconsistentNaming
            bool __isRight15(string number, out char rightBit)
            {
                rightBit = char.MinValue;
                return true;
            }

            // ReSharper disable once InconsistentNaming
            bool __isRight18(string number, out char rightBit)
            {
                var mod = ISO7064_1983.MOD11_2(idNumber.Select(c => (int)c - 48).Take(17).ToArray(), WeightFactors, 11);
                rightBit = CheckBits[mod];
                return rightBit == number[17];
            }
        }

        internal static bool ValidTheRest(string idNumber, ChinaIdNumberValidationOptions options, List<VerifyFailure> failures, ChinaIdNumberInfo info)
        {
            var strSequence = options.Length switch
            {
                ChinaIdLength.Id15 => __getSequence15Number(idNumber),
                ChinaIdLength.Id18 => __getSequence18Number(idNumber),
                _ => throw new ArgumentOutOfRangeException()
            };

            var sequence = int.Parse(strSequence);

            info.Gender = (ChinaIdGender)(sequence % 2);
            info.Sequence = sequence;

            return true;

            // ReSharper disable once InconsistentNaming
            string __getSequence15Number(string number)
            {
                return number.Substring(12, 3);
            }

            // ReSharper disable once InconsistentNaming
            string __getSequence18Number(string number)
            {
                return number.Substring(14, 3);
            }
        }

        #endregion
    }
}