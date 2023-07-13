using Newtonsoft.Json;

namespace JsonProperty.EFCore.Tests.GeneralTests
{
    internal class StrictTypeSettingsTest
    {
        private string[] vals = { "Item1", "Item2", "Item3", "Item4" };
        private JsonEnumerable<string>? Enumerable { get; set; }

        [SetUp]
        public void Setup()
        {
        }

        [Test, Order(0)]
        public void JsonSettingsTest()
        {
            Console.WriteLine(nameof(JsonSettingsTest));
            Assert.Throws<Exception>(() =>
            {
                Assert.IsFalse(Settings.JsonSettings.AllowChangeStrictParamAfterItUsed);
                _ = Settings.JsonSettings.StrictTypeSerialization;
                Settings.JsonSettings.StrictTypeSerialization = false;
            });
            Assert.DoesNotThrow(() =>
            {
                Settings.JsonSettings.AllowChangeStrictParamAfterItUsed = true;
                Settings.JsonSettings.StrictTypeSerialization = false;
            });
        }

        [Test, Order(1)]
        public void JsonSettingsTest1()
        {
            Console.WriteLine(nameof(JsonSettingsTest1));
            Assert.DoesNotThrow(() =>
            {
                Settings.JsonSettings.StrictTypeSerialization = false;
                Enumerable = new JsonEnumerable<string>();
                Enumerable.AddRange(vals);
                Assert.That(Enumerable.JsonString == JsonConvert.SerializeObject(vals));

                Settings.JsonSettings.StrictTypeSerialization = true;
                Enumerable = new JsonEnumerable<string>();
                Enumerable.AddRange(vals);
                Assert.That(Enumerable.JsonString != JsonConvert.SerializeObject(vals));

                Settings.JsonSettings.StrictTypeSerialization = true;
                Settings.JsonSettings.AllowChangeStrictParamAfterItUsed = false;
            });
        }
    }
}