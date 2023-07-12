using System.Reflection;

namespace JsonPropertyAdapter.Base.Details
{
    internal abstract class AbstractStringJsonPropertySerializer
    {
        protected Func<string?> GetProp;
        protected Action<string> SetProp;

        public AbstractStringJsonPropertySerializer(object parent) : this(parent, null)
        {
        }

        public AbstractStringJsonPropertySerializer(object parent, string? propName)
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
    }
}