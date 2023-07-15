using System.Collections.Immutable;

namespace JsonProperty.EFCore.Tests.Shared
{
    internal static class TestValues
    {
        public static IImmutableList<string> Strings { get; } = ImmutableArray.Create("Item1", "Item2", "Item3", "Item4");
    }
}