using Microsoft.EntityFrameworkCore;

namespace JsonPropertyAdapter
{
    [Owned]
    public class JsonDictionary : JsonDictionary<string, object>
    {
    }
}