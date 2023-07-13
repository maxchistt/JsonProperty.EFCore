using Newtonsoft.Json;

namespace JsonProperty.EFCore.Base.Serializers.CollectionSerializers.Strict.TypedJson
{
    internal static class TypePacker
    {
        public static object[] Pack(object value)
        {
            var valtype = new object[2];
            valtype[0] = value;
            valtype[1] = value.GetType().FullName ?? value.GetType().Name;
            return valtype;
        }

        public static object? Unpack(object[] typedValueCollection)
        {
            string TypeName = typedValueCollection[1]?.ToString() ??
                throw new ArgumentNullException("No string TypeName value");
            Type type = AssemblyTypeManager.ByName(TypeName);
            object Val = typedValueCollection[0];

            if (Val.GetType() == type)
            {
                return Val;
            }
            else
            {
                try
                {
                    return Convert.ChangeType(Val, type);
                }
                catch
                {
                    return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(Val), type);
                }
            }
        }
    }
}