using System.Text.Json;

namespace JsonProperty.EFCore.Tests.GeneralTests
{
    internal class TypeConversionTest
    {
        private const bool TestComplexTypeConversionStrictly = false;

        public JsonEnumerable list { get; set; } = new();

        [SetUp]
        public void Setup()
        {
        }

        [Test, Order(0)]
        public void TestBasicConversion()
        {
            Console.WriteLine(nameof(TestBasicConversion));

            list.Add("str");
            list.Add(2.5);

            Assert.That(list.Deserialize().First(), Is.InstanceOf<string>());
            Assert.That(list.Deserialize().Last(), Is.InstanceOf<double>());
        }

        [Test, Order(1)]
        public void SubitemComplexTypeConversionTestV1()
        {
            Console.WriteLine(nameof(SubitemComplexTypeConversionTestV1));

            //Create single product
            Product product = new()
            {
                Name = "Car",
                Parameters ={VirtualDictionary = new Dictionary<string,object>() {
                    {"Price",10099.99m },{"MaxSpeed",300},{ "Engine capacity",6},{"ElectroCar",false}
                }}
            };

            //list
            list.Add(product);

            var listLast = list.Deserialize().Last();
            Console.WriteLine($"List last:\r\n {listLast} {listLast.GetType()}");

            bool isTypeProdict = listLast is Product;
            Console.WriteLine($"listLast is Product:\r\n {isTypeProdict}");
            Assert.That(isTypeProdict, "listLast is Product type test failed");

            var prod = listLast as Product;
            Console.WriteLine($"listLast as Product:\r\n {prod}");
            Console.WriteLine($"Last sublist product name:\r\n {prod.Name}");
            Console.WriteLine($"Last sublist product params:\r\n {prod.Parameters.JsonString}\r\n");

            var lastParamValue = prod.Parameters.Deserialize().Last().Value;
            Console.WriteLine($"lastParamValue:\r\n {lastParamValue}\r\nlastParamValue type:\r\n {lastParamValue.GetType()}");
            bool lastParamValueIsBool = lastParamValue is bool;
            Console.WriteLine($"lastParamValue is bool:\r\n {lastParamValueIsBool}\r\n");
            Assert.That(lastParamValueIsBool || lastParamValue is JsonElement, "lastParamValue is bool or JsonElementtest failed");
        }

        [Test, Order(2)]
        public void SubitemComplexTypeConversionTestV2()
        {
            Console.WriteLine(nameof(SubitemComplexTypeConversionTestV2));

            var prod_listLast = list.Deserialize().Last() as Product;
            object Test1Res_lastParamValue = prod_listLast.Parameters.Deserialize().Last().Value;

            if (Test1Res_lastParamValue.GetType() == typeof(object))
                Assert.Fail($"{nameof(Test1Res_lastParamValue)} is object");

            if (Test1Res_lastParamValue is JsonElement jsonElement)
            {
                Console.WriteLine("lastParamValue is JsonElement");
                Console.WriteLine($"jsonElement.ValueKind = {jsonElement.ValueKind}");

                bool isKindTrue = jsonElement.ValueKind == JsonValueKind.True;
                bool isKindFalse = jsonElement.ValueKind == JsonValueKind.False;
                bool isKindObject = jsonElement.ValueKind == JsonValueKind.Object;
                bool isKindString = jsonElement.ValueKind == JsonValueKind.String;
                Console.WriteLine($"{nameof(isKindTrue)} = {isKindTrue} | {nameof(isKindFalse)} = {isKindFalse} | {nameof(isKindObject)} = {isKindObject} | {nameof(isKindString)} = {isKindString}");

                Assert.That(isKindTrue || isKindFalse, "isKindTrue||isKindFalse if is JsonElement test fail");
            }
            else
            {
                Console.WriteLine("lastParamValue is NOT JsonElement");
                Assert.That(Test1Res_lastParamValue is bool, "Test1Res_lastParamValue is bool if is NOT JsonElement test failed");
            }
        }

        [Test, Order(3)]
        public void SubitemComplexTypeConversionTestV3()
        {
            Console.WriteLine(nameof(SubitemComplexTypeConversionTestV3));

            var prod_listLast = list.Deserialize().Last() as Product;
            object Test1Res_lastParamValue = prod_listLast.Parameters.Deserialize().Last().Value;

            if (Test1Res_lastParamValue.GetType() == typeof(object))
                Assert.Fail($"{nameof(Test1Res_lastParamValue)} is object");

            if (TestComplexTypeConversionStrictly)
            {
                Assert.That(Test1Res_lastParamValue, Is.InstanceOf(typeof(bool)), "lastParamValue is bool test failed");
            }
            else
            {
                Console.WriteLine($"!!! {nameof(SubitemComplexTypeConversionTestV3)} test is disabled by {nameof(TestComplexTypeConversionStrictly)} = {TestComplexTypeConversionStrictly} const");
            }
        }

        public class Product
        {
            public string? Name { get; set; }

            public JsonDictionary Parameters { get; set; } = new();
        }
    }
}