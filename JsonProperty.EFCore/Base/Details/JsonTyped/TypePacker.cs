using System.Text.Json;

namespace JsonProperty.EFCore.Base.Details.JsonTyped
{
    internal static class TypePacker
    {
        public static object[] Pack(object value)
        {
            var valtype = new object[2];
            valtype[0] = JsonSerializer.Deserialize<JsonElement>(JsonSerializer.Serialize(value));
            valtype[1] = value.GetType().FullName ?? value.GetType().Name;
            return valtype;
        }

        public static object? Unpack(object[] typedValueCollection)
        {
            string TypeName = (string)typedValueCollection[1].ToString();
            Type type = AssemblyTypeManager.ByName(TypeName);
            JsonElement Val = (JsonElement)typedValueCollection[0];
            return Val.Deserialize(type);
        }
    }
}