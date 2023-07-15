using JsonProperty.EFCore.Tests.Shared;

namespace JsonProperty.EFCore.Tests.TestsByType
{
    internal class ListTest
    {
        public JsonList<string> List { get; set; }

        [SetUp]
        public void Setup()
        {
            List = new();
        }

        [Test]
        public void TestList()
        {
            Console.WriteLine(nameof(TestList));
            Assert.DoesNotThrow(() =>
            {
                List.Add(TestValues.StringsList[0]);

                List.Edit(en =>
                {
                    en.Add(TestValues.StringsList[1]);
                    return en;
                });

                List.AddRange(new string[] { TestValues.StringsList[2], TestValues.StringsList[3] });

                for (int i = 0; i < TestValues.StringsList.Count; i++)
                {
                    Assert.IsTrue(TestValues.StringsList[i] == List.Deserialize().ElementAt(i), "1) Assert.IsTrue(TestValues.Strings[i] == Enumerable.Deserialize().ElementAt(i)");
                }

                List.Serialize(TestValues.StringsList.ToList());

                for (int i = 0; i < TestValues.StringsList.Count; i++)
                {
                    Assert.IsTrue(TestValues.StringsList[i] == List.VirtualList.ElementAt(i), "2) TestValues.Strings[i] == Enumerable.VirtualEnumerable.ElementAt(i)");
                }
            });
        }
    }
}