using JsonProperty.EFCore.Tests.Shared;

namespace JsonProperty.EFCore.Tests.TestsByType
{
    internal class ItemTest
    {
        public JsonItem<string> Item { get; set; }

        [SetUp]
        public void Setup()
        {
            Item = new();
        }

        [Test]
        public void TestItem()
        {
            Console.WriteLine(nameof(TestItem));
            Assert.DoesNotThrow(() =>
            {
                Item.Edit(en => TestValues.StringsList[1]);

                Assert.IsTrue(TestValues.StringsList[1] == Item.Deserialize(), "1) Assert.IsTrue(TestValues.StringsList[1] == Item.Deserialize()");

                Item.Serialize(TestValues.StringsList[2]);

                Assert.IsTrue(TestValues.StringsList[2] == Item.VirtualItem, "2) Assert.IsTrue(TestValues.StringsList[2] == Item.VirtualElement");
            });
        }
    }
}