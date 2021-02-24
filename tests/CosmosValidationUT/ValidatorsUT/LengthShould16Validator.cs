using System;
using System.Collections.Generic;
using System.Linq;
using Cosmos.Validation;
using Cosmos.Validation.Annotations;
using Cosmos.Validation.Internals.Extensions;
using Cosmos.Validation.Objects;
using Cosmos.Validation.Validators;

namespace CosmosValidationUT.ValidatorsUT
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class LengthShould16Attribute : Attribute, IFlagAnnotation { }

    public class Length16Model
    {
        [LengthShould16] public string Name { get; set; }
    }

    public class LengthShould16Validator : CustomValidator
    {
        private ValidationOptions _options { get; set; }

        public LengthShould16Validator()
            : base("LengthShould16Validator")
        {
            _options = new ValidationOptions();
        }

        public LengthShould16Validator(ValidationOptions options)
            : base("LengthShould16Validator")
        {
            _options = options ?? new ValidationOptions();
        }

        protected override VerifyResult VerifyImpl(ObjectContext context)
        {
            if (context is null)
                return _options.ReturnNullReferenceOrSuccess();

            List<VerifyResult> results = new();
            var values = context.GetValues();

            foreach (var value in values)
            {
                // 如果 Value 为 String，对其进行验证
                if (value.Value is string str)
                {
                    var attr = value.GetAttributes<LengthShould16Attribute>().FirstOrDefault();

                    if (attr is null)
                        continue;

                    if (str.Length != 16)
                        results.Add(new VerifyResult(new VerifyFailure(value.MemberName, "Length should 16.", str)));
                }
                // 否则，如果 Value 不是基础类型（即 Value 为引用类型、结构等），对其进一步解析并验证
                else if (!value.BasicTypeState())
                {
                    results.Add(VerifyImpl(value.ConvertToObjectContext()));
                }
            }
            
            return VerifyResult.MakeTogether(results);
        }

        protected override VerifyResult VerifyOneImpl(ObjectValueContext context)
        {
            if (context is null)
                return _options.ReturnNullReferenceOrSuccess();

            // 如果 Value 为 String，对其进行验证
            if (context.Value is string str)
            {
                var attr = context.GetAttributes<LengthShould16Attribute>().FirstOrDefault();
                if (attr is null)
                    return VerifyResult.NullReferenceWith("There's no LengthShould16Attribute on this Member.");
                if (str.Length == 16)
                    return VerifyResult.Success;
                return new VerifyResult(new VerifyFailure(context.MemberName, "Length should 16.", str));
            }

            // 否则，如果 Value 不是基础类型（即 Value 为引用类型、结构等），对其进一步解析并验证
            if (!context.BasicTypeState())
            {
                return VerifyImpl(context.ConvertToObjectContext());
            }

            // 否则，认为其类型不是所期待的
            return VerifyResult.UnexpectedTypeWith(context.MemberName);
        }
    }
}