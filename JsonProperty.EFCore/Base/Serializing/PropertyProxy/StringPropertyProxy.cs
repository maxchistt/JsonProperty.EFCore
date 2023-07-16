using JsonProperty.EFCore.Base.Interfaces.PropertyProxy;
using System.Reflection;

namespace JsonProperty.EFCore.Base.Serializing.PropertyProxy
{
    internal sealed class StringPropertyProxy : IStringPropertyProxy
    {
        private readonly Func<string?> GetProp;
        private readonly Action<string> SetProp;

        public StringPropertyProxy(object parent) : this(parent, null)
        {
        }

        public StringPropertyProxy(object parent, string? propName)
        {
            if (string.IsNullOrEmpty(propName))
            {
                propName = parent.GetType().GetProperties().Where(p => p.PropertyType == typeof(string)).First().Name;
            }

            PropertyInfo propInf = parent.GetType().GetProperty(propName) ??
                throw new Exception("Property find by name error");
            if (propInf.PropertyType != typeof(string))
                throw new Exception("Property has not a string type");

            GetProp = () =>
            {
                return (string?)propInf.GetValue(parent);
            };
            SetProp = (str) =>
            {
                propInf.SetValue(parent, str);
            };
        }

        public string? Get()
        {
            return GetProp.Invoke();
        }

        public void Set(string value)
        {
            SetProp.Invoke(value);
        }
    }
}