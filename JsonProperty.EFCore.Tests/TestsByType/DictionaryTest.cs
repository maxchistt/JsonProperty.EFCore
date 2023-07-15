using JsonProperty.EFCore.Tests.Shared;

namespace JsonProperty.EFCore.Tests.TestsByType
{
    internal class DictionaryTest
    {
        public JsonDictionary<string, string> Dictionary { get; set; }

        [SetUp]
        public void Setup()
        {
            Dictionary = new();
        }

        [Test]
        public void TestDictionary()
        {
            Console.WriteLine(nameof(TestDictionary));
            Assert.DoesNotThrow(() =>
            {
                Dictionary.Add(TestValues.StringsDictionary.ElementAt(0));
                Dictionary.Edit(en => en.Append(TestValues.StringsDictionary.ElementAt(1)));
                Dictionary.AddRange(new Dictionary<string, string>(new[]{
                    TestValues.StringsDictionary.ElementAt(2),TestValues.StringsDictionary.ElementAt(3)
                }));

                for (int i = 0; i < TestValues.StringsList.Count; i++)
                {
                    Assert.IsTrue(TestValues.StringsDictionary.ElementAt(i).Value == Dictionary.Deserialize().ElementAt(i).Value, "1) TestValues.StringsDictionary.ElementAt(i).Value == Dictionary.Deserialize().ElementAt(i).Value");
                }

                Dictionary.Serialize(new Dictionary<string, string>(TestValues.StringsDictionary));

                for (int i = 0; i < TestValues.StringsList.Count; i++)
                {
                    Assert.IsTrue(TestValues.StringsDictionary.ElementAt(i).Value == Dictionary.VirtualDictionary.ElementAt(i).Value, "2) TestValues.StringsDictionary.ElementAt(i).Value == Dictionary.VirtualDictionary.ElementAt(i).Value");
                }
            });
        }
    }
}