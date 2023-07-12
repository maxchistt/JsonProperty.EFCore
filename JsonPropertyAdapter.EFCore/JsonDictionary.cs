using Microsoft.EntityFrameworkCore;

namespace JsonPropertyAdapter.EFCore
{
    [Owned]
    public class JsonDictionary : JsonDictionary<string, object>
    {
    }
}