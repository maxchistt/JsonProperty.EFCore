namespace JsonProperty.EFCore.Tests
{
    internal class TypeConversionTest
    {
        public JsonEnumerable list { get; set; }

        [SetUp]
        public void Setup()
        {
            list = new();
        }

        [Test]
        public void Test()
        {
            list.Add("str");
            list.Add(2.5);

            Assert.That(list.Deserialize().First(), Is.InstanceOf<string>());
            Assert.That(list.Deserialize().Last(), Is.InstanceOf<double>());
        }
    }
}