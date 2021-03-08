using Cosmos.Validation.Internals;
using Cosmos.Validation.Internals.Rules;
using Cosmos.Validation.Registrars;
using CosmosValidationUT.Models;
using CosmosValidationUT.StrategyUT;
using FluentAssertions;
using Shouldly;
using Xunit;

namespace CosmosValidationUT.BuildingUT
{
    [Trait("BuildingUT", "TakingEffect")]
    public class TakingEffectTests
    {
        [Fact(DisplayName = "Strategy level register ops and take effect test")]
        public void StrategyLevelTakeEffectTest()
        {
            var registrar = ValidationRegistrar.Continue();
            registrar.ShouldNotBeNull();

            var exposedPoint1 = (registrar as InternalValidationRegistrar)?.GetCorrectValueRulesForUnitTests();
            var _typedRulesDictionary1 = exposedPoint1.Value.Item1;
            var _namedRulesDictionary1 = exposedPoint1.Value.Item2;

            _typedRulesDictionary1.ShouldNotBeNull();
            _namedRulesDictionary1.ShouldNotBeNull();

            _typedRulesDictionary1.Keys.Should().HaveCount(0);
            _namedRulesDictionary1.Keys.Should().HaveCount(0);

            registrar.ForStrategy<NormalNiceBoatStrategy>().TakeEffect();
            var exposedPoint2 = (registrar as InternalValidationRegistrar)?.GetCorrectValueRulesForUnitTests();

            exposedPoint2.ShouldNotBeNull();
            var _typedRulesDictionary2 = exposedPoint2.Value.Item1;
            var _namedRulesDictionary2 = exposedPoint2.Value.Item2;

            _typedRulesDictionary2.ShouldNotBeNull();
            _namedRulesDictionary2.ShouldNotBeNull();

            _typedRulesDictionary2.Keys.Should().HaveCount(1);
            _namedRulesDictionary2.Keys.Should().HaveCount(0);

            var rules = _typedRulesDictionary2[typeof(NiceBoat)];

            rules.Should().HaveCount(3);
            rules[0].Should().NotBeNull()
                    .And.Match<CorrectValueRule>(x => x.MemberName == "Name")
                    .And.Match<CorrectValueRule>(x => x.Mode == CorrectValueRuleMode.Append)
                    .And.Match<CorrectValueRule>(x => x.Tokens.Count == 1)
                    .And.Match<CorrectValueRule>(x => x.Tokens[0].Ops == CorrectValueOps.NotEmpty);


            rules[1].Should().NotBeNull()
                    .And.Match<CorrectValueRule>(x => x.MemberName == "Length")
                    .And.Match<CorrectValueRule>(x => x.Mode == CorrectValueRuleMode.Append)
                    .And.Match<CorrectValueRule>(x => x.Tokens.Count == 1)
                    .And.Match<CorrectValueRule>(x => x.Tokens[0].Ops == CorrectValueOps.GreaterThanOrEqual);

            rules[2].Should().NotBeNull()
                    .And.Match<CorrectValueRule>(x => x.MemberName == "Width")
                    .And.Match<CorrectValueRule>(x => x.Mode == CorrectValueRuleMode.Append)
                    .And.Match<CorrectValueRule>(x => x.Tokens.Count == 1)
                    .And.Match<CorrectValueRule>(x => x.Tokens[0].Ops == CorrectValueOps.GreaterThanOrEqual);
        }

        [Fact(DisplayName = "Type and member level register ops and take effect test")]
        public void TypeAndMemberLevelTakeEffectTest()
        {
            var registrar = ValidationRegistrar.Continue();
            registrar.ShouldNotBeNull();

            var exposedPoint1 = (registrar as InternalValidationRegistrar)?.GetCorrectValueRulesForUnitTests();
            var _typedRulesDictionary1 = exposedPoint1.Value.Item1;
            var _namedRulesDictionary1 = exposedPoint1.Value.Item2;

            _typedRulesDictionary1.ShouldNotBeNull();
            _namedRulesDictionary1.ShouldNotBeNull();

            _typedRulesDictionary1.Keys.Should().HaveCount(0);
            _namedRulesDictionary1.Keys.Should().HaveCount(0);

            registrar.ForType<NiceBoat>().ForMember(x => x.Name).NotEmpty()
                     .AndForMember(x => x.Length).GreaterThanOrEqual(0)
                     .AndForMember(x => x.Width).GreaterThanOrEqual(0);

            var exposedPoint2 = (registrar as InternalValidationRegistrar)?.GetCorrectValueRulesForUnitTests();
            var _typedRulesDictionary2 = exposedPoint2.Value.Item1;
            var _namedRulesDictionary2 = exposedPoint2.Value.Item2;

            _typedRulesDictionary2.ShouldNotBeNull();
            _namedRulesDictionary2.ShouldNotBeNull();

            _typedRulesDictionary2.Keys.Should().HaveCount(0);
            _namedRulesDictionary2.Keys.Should().HaveCount(0);

            registrar.ForType<NiceBoat>().ForMember(x => x.Name).NotEmpty()
                     .AndForMember(x => x.Length).GreaterThanOrEqual(0)
                     .AndForMember(x => x.Width).GreaterThanOrEqual(0)
                     .TakeEffect();

            var exposedPoint3 = (registrar as InternalValidationRegistrar)?.GetCorrectValueRulesForUnitTests();

            exposedPoint3.ShouldNotBeNull();
            var _typedRulesDictionary3 = exposedPoint3.Value.Item1;
            var _namedRulesDictionary3 = exposedPoint3.Value.Item2;

            _typedRulesDictionary3.ShouldNotBeNull();
            _namedRulesDictionary3.ShouldNotBeNull();

            _typedRulesDictionary3.Keys.Should().HaveCount(1);
            _namedRulesDictionary3.Keys.Should().HaveCount(0);

            var rules = _typedRulesDictionary3[typeof(NiceBoat)];

            rules.Should().HaveCount(3);
            rules[0].Should().NotBeNull()
                    .And.Match<CorrectValueRule>(x => x.MemberName == "Name")
                    .And.Match<CorrectValueRule>(x => x.Mode == CorrectValueRuleMode.Append)
                    .And.Match<CorrectValueRule>(x => x.Tokens.Count == 1)
                    .And.Match<CorrectValueRule>(x => x.Tokens[0].Ops == CorrectValueOps.NotEmpty);


            rules[1].Should().NotBeNull()
                    .And.Match<CorrectValueRule>(x => x.MemberName == "Length")
                    .And.Match<CorrectValueRule>(x => x.Mode == CorrectValueRuleMode.Append)
                    .And.Match<CorrectValueRule>(x => x.Tokens.Count == 1)
                    .And.Match<CorrectValueRule>(x => x.Tokens[0].Ops == CorrectValueOps.GreaterThanOrEqual);

            rules[2].Should().NotBeNull()
                    .And.Match<CorrectValueRule>(x => x.MemberName == "Width")
                    .And.Match<CorrectValueRule>(x => x.Mode == CorrectValueRuleMode.Append)
                    .And.Match<CorrectValueRule>(x => x.Tokens.Count == 1)
                    .And.Match<CorrectValueRule>(x => x.Tokens[0].Ops == CorrectValueOps.GreaterThanOrEqual);
        }
    }
}