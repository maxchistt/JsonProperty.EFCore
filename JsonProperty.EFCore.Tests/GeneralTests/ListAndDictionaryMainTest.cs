namespace JsonProperty.EFCore.Tests.GeneralTests
{
    internal class ListAndDictionaryMainTest
    {
        [SetUp]
        public void SetUp()
        { }

        [Test]
        public void MainTest()
        {
            Console.WriteLine(nameof(MainTest));
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

                var lastProductParamsStr = products.Last().Parameters.JsonString;
                Console.WriteLine($"\r\nCar product params json str:\r\n {lastProductParamsStr}\r\n");

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

                var listDeserialisedCol = list.Deserialize();
                Console.WriteLine($"List deserialized collection:\r\n {listDeserialisedCol}\r\n");

                var jsonListString = list.JsonString;
                Console.WriteLine($"List json sting:\r\n {jsonListString}\r\n");

                var listLast = list.Deserialize().Last();
                Console.WriteLine($"List last:\r\n {listLast}\r\nList last type:\r\n {listLast.GetType()}");
                Console.WriteLine($"typeof(Product):\r\n {typeof(Product)}\r\n");

                bool isTypeProdict = listLast is Product;
                Console.WriteLine($"listLast is Product:\r\n {isTypeProdict}");

                var prod = listLast as Product;
                Console.WriteLine($"listLast as Product:\r\n {prod}");
                Console.WriteLine($"Last sublist product name:\r\n {prod.Name}\r\nLast sublist product params:\r\n {prod.Parameters.JsonString}\r\n");
            });
        }

        public class Product
        {
            public string? Name { get; set; }

            public JsonDictionary Parameters { get; set; } = new();
        }
    }
}