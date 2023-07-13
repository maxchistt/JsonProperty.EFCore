namespace JsonProperty.EFCore.Tests
{
    internal class ListAndDictionaryMainTest
    {
        [SetUp]
        public void SetUp()
        { }

        [Test]
        public void MainTest()
        {
            Assert.DoesNotThrow(() =>
            {
                //Create array of products
                List<Product> products = new()
                {
                    new() {Name="Phone",Parameters={
                        VirtualDictionary = new Dictionary<string,object>() {
                                {"Camera",13.5 },{"OS","Android" },{"Screen","1080x900"},{"Storage",32}
                            }
                        }
                    },
                    new() {Name="Car",Parameters={
                        VirtualDictionary = new Dictionary<string,object>() {
                                {"Price",10099.99m },{"MaxSpeed",300},{ "Engine capacity",6},{"ElectroCar",false}
                            }
                        }
                    }
                };

                //Create single product
                var product = new Product() { Name = "Bag" };
                product.Parameters.AddRange(new Dictionary<string, object>() {
                    { "Voliune", 5 }, { "Color", "Red" }
                });
                product.Parameters.Add("Matherial", "Leather");

                //list
                JsonEnumerable list = new();
                list.Add("str");
                list.Add(2.5);
                list.Add(product);
                list.AddRange(products);
                var a = list.Deserialize();
                Console.WriteLine(a);
            });
        }

        public class Product
        {
            public string? Name { get; set; }

            public JsonDictionary Parameters { get; set; } = new();
        }
    }
}