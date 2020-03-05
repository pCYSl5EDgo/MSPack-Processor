namespace ComplexTestClasses
{
    public struct ImplementorStruct : IUnionBase
    {
        public ImplementorStruct(int a)
        {
            Value = a;
        }

        public int Value { get; }
    }
}