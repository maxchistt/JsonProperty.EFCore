using Newtonsoft.Json;

namespace JsonProperty.EFCore.Base.JsonTyped
{
    internal static class TypePacker
    {
        public static object[] Pack(object value)
        {
            var valtype = new object[2];
            valtype[0] = JsonConvert.DeserializeObject<object>(JsonConvert.SerializeObject(value));
            valtype[1] = value.GetType().FullName ?? value.GetType().Name;
            return valtype;
        }

        public static object? Unpack(object[] typedValueCollection)
        {
            string TypeName = typedValueCollection[1].ToString();
            Type type = AssemblyTypeManager.ByName(TypeName);
            object Val = (object)typedValueCollection[0];
            return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(Val), type);
        }
    }
}