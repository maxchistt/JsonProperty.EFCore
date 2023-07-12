using JsonPropertyAdapter.EFCore.Base;
using Microsoft.EntityFrameworkCore;

namespace JsonPropertyAdapter.EFCore
{
    [Owned]
    public class JsonDictionary : JsonDictionaryBase<string, object>
    {
    }
}