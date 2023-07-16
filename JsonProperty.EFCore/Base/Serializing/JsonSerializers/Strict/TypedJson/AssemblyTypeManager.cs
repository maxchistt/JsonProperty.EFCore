namespace JsonProperty.EFCore.Base.Serializing.JsonSerializers.Strict.TypedJson
{
    internal static class AssemblyTypeManager
    {
        private static Dictionary<string, Type> TypesMemCache { get; } = new();

        public static Type ByName(string name)
        {
            if (TypesMemCache.ContainsKey(name)) return TypesMemCache[name];

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies().Reverse())
            {
                var tt = assembly.GetType(name);
                if (tt != null)
                {
                    TypesMemCache.Add(name, tt);
                    return tt;
                }
            }

            return null;
        }
    }
}