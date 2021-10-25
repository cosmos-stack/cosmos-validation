using CosmosStack.Validation.Annotations;

namespace CosmosValidationUT.Models
{
    public struct NiceStruct
    {
        public string Name { get; }

        public int Age { get; }

        public NiceStruct(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }

    public struct NiceStructWithAnnotation
    {
        [NotWhiteSpace] public string Name { get; }

        [NotNegative] public int Age { get; }

        public NiceStructWithAnnotation(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }
}