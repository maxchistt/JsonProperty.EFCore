using System.Collections.Immutable;

namespace JsonProperty.EFCore.Tests.Shared
{
    internal static class TestValues
    {
        public static IImmutableList<string> StringsList { get; } =
            ImmutableArray.Create(new string[] {
                "Item1", "Item2", "Item3", "Item4"
            });

        public static IImmutableDictionary<string, string> StringsDictionary { get; } =
            ImmutableDictionary.ToImmutableDictionary(new Dictionary<string, string> {
                { "Item1", "Item1" }, { "Item2", "Item2" }, { "Item3", "Item3" }, { "Item4", "Item4" }
            });
    }
}