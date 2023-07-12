using Microsoft.EntityFrameworkCore;

namespace JsonProperty.EFCore
{
    [Owned]
    public class JsonDictionary : JsonDictionary<string, object>
    {
    }
}