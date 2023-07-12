namespace JsonProperty.EFCore.Tests
{
    internal abstract class TestBase
    {
        public TestSet Set { get; set; }

        [SetUp]
        public void Setup()
        {
            Set = new();
        }

        public class TestSet
        {
            public JsonEnumerable<string> Enumerable { get; set; } = new();
            public JsonList<string> List { get; set; } = new();
            public JsonDictionary<string, string> Dictionary { get; set; } = new();
        }
    }
}