using JsonProperty.EFCore.Tests.Shared;
using System.Text;

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
                Enumerable.Add(TestValues.Strings[0]);
                Enumerable.Edit(en => en.Append(TestValues.Strings[1]));
                Enumerable.AddRange(new string[] { TestValues.Strings[2], TestValues.Strings[3] });

                for (int i = 0; i < TestValues.Strings.Count; i++)
                {
                    Assert.IsTrue(TestValues.Strings[i] == Enumerable.Deserialize().ElementAt(i), "1) Assert.IsTrue(TestValues.Strings[i] == Enumerable.Deserialize().ElementAt(i)");
                }

                Enumerable.Serialize(TestValues.Strings);

                for (int i = 0; i < TestValues.Strings.Count; i++)
                {
                    Assert.IsTrue(TestValues.Strings[i] == Enumerable.VirtualEnumerable.ElementAt(i), "2) TestValues.Strings[i] == Enumerable.VirtualEnumerable.ElementAt(i)");
                }
            });
        }
    }
}