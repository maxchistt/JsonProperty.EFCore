using JsonProperty.EFCore.Tests.Shared;

namespace JsonProperty.EFCore.Tests.TestsByType
{
    internal class EnumerableTest
    {
        public JsonEnumerable<string> Enumerable { get; set; }

        [SetUp]
        public void Setup()
        {
            Enumerable = new();
        }

        [Test]
        public void TestEnumerable()
        {
            Console.WriteLine(nameof(TestEnumerable));
            Assert.DoesNotThrow(() =>
            {
                Enumerable.Add(TestValues.StringsList[0]);
                Enumerable.Edit(en => en.Append(TestValues.StringsList[1]));
                Enumerable.AddRange(new string[] { TestValues.StringsList[2], TestValues.StringsList[3] });

                for (int i = 0; i < TestValues.StringsList.Count; i++)
                {
                    Assert.IsTrue(TestValues.StringsList[i] == Enumerable.Deserialize().ElementAt(i), "1) Assert.IsTrue(TestValues.Strings[i] == Enumerable.Deserialize().ElementAt(i)");
                }

                Enumerable.Serialize(TestValues.StringsList);

                for (int i = 0; i < TestValues.StringsList.Count; i++)
                {
                    Assert.IsTrue(TestValues.StringsList[i] == Enumerable.VirtualEnumerable.ElementAt(i), "2) TestValues.Strings[i] == Enumerable.VirtualEnumerable.ElementAt(i)");
                }
            });
        }
    }
}