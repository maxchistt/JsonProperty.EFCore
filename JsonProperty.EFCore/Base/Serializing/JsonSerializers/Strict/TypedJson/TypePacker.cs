using Newtonsoft.Json;

namespace JsonProperty.EFCore.Base.Serializing.JsonSerializers.Strict.TypedJson
{
    internal static class TypePacker
    {
        public static object[] Pack(object? value)
        {
            var valtype = new object[2];
            valtype[0] = value!;
            valtype[1] = value?.GetType().FullName ?? value?.GetType().Name ?? typeof(object).FullName!;
            return valtype;
        }

        public static object? Unpack(object[] typedValueCollection)
        {
            string? TypeName = typedValueCollection[1]?.ToString();
            if (TypeName is null)
                throw new ArgumentNullException("No typedValueCollection[1] string TypeName param");
            Type type = AssemblyTypeManager.ByName(TypeName ?? typeof(object).FullName!);
            object? Value = typedValueCollection[0];

            if (Value?.GetType() == type || Value is null)
            {
                return Value;
            }
            else
            {
                try
                {
                    return Convert.ChangeType(Value, type);
                }
                catch
                {
                    return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(Value), type);
                }
            }
        }
    }
}