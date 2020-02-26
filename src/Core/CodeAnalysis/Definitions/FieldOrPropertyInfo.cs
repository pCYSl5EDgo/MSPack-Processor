namespace MSPack.Processor.Core.Definitions
{
    public readonly struct FieldOrPropertyInfo
    {
        public readonly IndexerAccessResult Result;
        public readonly FieldSerializationInfo Field;
        public readonly PropertySerializationInfo Property;

        public FieldOrPropertyInfo(FieldSerializationInfo fieldSerializationInfo)
        {
            Result = IndexerAccessResult.Field;
            Field = fieldSerializationInfo;
            Property = default;
        }

        public FieldOrPropertyInfo(PropertySerializationInfo propertySerializationInfo)
        {
            Result = IndexerAccessResult.Property;
            Field = default;
            Property = propertySerializationInfo;
        }

        public void Deconstruct(out IndexerAccessResult result, out FieldSerializationInfo field, out PropertySerializationInfo property)
        {
            result = Result;
            field = Field;
            property = Property;
        }
    }
}
