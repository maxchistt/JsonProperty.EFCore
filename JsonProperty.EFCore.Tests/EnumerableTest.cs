using System.Text;
using System.Text.Json;

namespace JsonProperty.EFCore.Tests
{
    internal class EnumerableTest : TestBase
    {
        private string[] vals = { "Item1", "Item2", "Item3", "Item4" };

        [Test]
        public void TestEnumerable()
        {
            Set.Enumerable.Add(vals[0]);
            Set.Enumerable.Edit(en => en.Append(vals[1]));
            Set.Enumerable.AddRange(new string[] { vals[2], vals[3] });

            for (int i = 0; i < vals.Length; i++)
            {
                Assert.IsTrue(vals[i] == Set.Enumerable.Deserialize().ElementAt(i));
            }

            Assert.That(Set.Enumerable.JsonString == JsonSerializer.Serialize(vals));

            Set.Enumerable.Serialize(vals);

            for (int i = 0; i < vals.Length; i++)
            {
                Assert.IsTrue(vals[i] == Set.Enumerable.VirtualEnumerable.ElementAt(i));
            }
        }
    }
}