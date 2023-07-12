using DEMO.Context;
using DEMO.Models;

RecreateDatabase();
FillData();
PrintData();

void RecreateDatabase()
{
    using (var db = new DataContext())
    {
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();
        Console.WriteLine("DB Recreated");
    }
}

void FillData()
{
    using (var db = new DataContext())
    {
        //Create array of products
        List<Product> products = new()
        {
            new() {Name="Phone",Price=500.95m,Amount=21,Parameters={
                    VirtualDictionary = new Dictionary<string,object>() {
                        {"Camera",13.5 },{"OS","Android" },{"Screen","1080x900"},{"Storage",32}
                    }
                }
            },
            new() {Name="Car",Price=100000m,Amount=3,Parameters={
                    VirtualDictionary = new Dictionary<string,object>() {
                        {"MaxSpeed",300},{ "Engine capacity",6},{"ElectroCar",false}
                    }
                }
            }
        };

        //Add products
        db.Goods.AddRange(products);

        //Create single product
        var product = new Product() { Name = "Bag" };
        product.Price = 399.99m;
        product.Amount = 7;
        product.Parameters.AddRange(new Dictionary<string, object>() {
            { "Voliune", 5 }, { "Color", "Red" }
        });
        product.Parameters.Add("Matherial", "Leather");

        //Add product
        db.Goods.Add(product);

        //Save entities
        db.SaveChanges();

        Console.WriteLine("Demo data added");
    }
}

void PrintData()
{
    using (DataContext db = new DataContext())
    {
        Console.WriteLine("\r\nShowing goods from DB:");
        foreach (var product in db.Goods.ToList())
        {
            Console.WriteLine($"{Indent(1)} Product:{product.Name} Id: {product.Id}");
            Console.WriteLine($"{Indent(2)} Price: {product.Price}");
            Console.WriteLine($"{Indent(2)} Amount: {product.Amount}");
            Console.WriteLine($"{Indent(2)} ParametersJSON: {product.Parameters.JsonString}");
            if (product.Parameters.JsonDictionaryDeserialize().Any())
            {
                Console.WriteLine($"{Indent(3)} Parameters items:");
                foreach (var keyValuePair in product.Parameters.JsonDictionaryDeserialize())
                {
                    Console.WriteLine($"{Indent(4)} Key: '{keyValuePair.Key}' Value: '{keyValuePair.Value}' Type: '{keyValuePair.Value.GetType().Name}'");
                }
            }
        }
        Console.WriteLine();
    }

    string Indent(ushort indent) => indent switch
    {
        0 => "",
        1 => " ---",
        _ => new string(' ', indent + 1) + "-"
    };
}