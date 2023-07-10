# JsonPropertyAdapter

## About

This project allows you to use JSON fields in EF Core without setting up Fluent API

| Id  | Name  | Price  | Params                                                             |
| --- | ----- | ------ | ------------------------------------------------------------------ |
| 1   | Phone | 500    | {"Camera":13.5,"OS":"Android 11","Screen":"1080x900","Storage":32} |
| 2   | Car   | 100000 | {"MaxSpeed":300,"Engine capacity":6,"ElectroCar":false}            |
| 3   | Bag   | 400    | {"Voliume":5,"Color":"Red"}                                        |

## Instruction

Here are few steps how to use JsonPropertyAdapter project:

1. **Connect the project and specify 'using'**

   ```cs
    using JsonPropertyAdapter;
   ```

1. **Create entity model**

   ```cs
    public class Note
    {
        [Key, Required]
        public int Id { get; set; }
        public string? Header { get; set; }
    }
   ```

   Or

   ```cs
    public class Product
    {
        [Key, Required]
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
    }
   ```

1. **Create a JSON collection item class if nessesary**

   ```cs
    public class TodoItem
    {
        public string? Text { get; set; }
        public bool? CompleteStatus { get; set; }
    }
   ```

1. **Create a JSON field class that inherits from the `JsonEnumerableAdaptable` or `JsonDictionaryAdaptable` class with one <span style="text-decoration:underline">string</span> property and an <span style="text-decoration:underline">"Owned"</span> attribute.**

   ```cs
    [Owned]
    public class JsonTodoItems : JsonEnumerableAdaptable<TodoItem>
    {
        [Column("JsonTodoItemsList")]
        public string? JsonListString { get; set; }
    }
   ```

   Or

   ```cs
    [Owned]
    public class JsonParamsDictionary : JsonDictionaryAdaptable<string, object>
    {
        [Column("JsonParametersDictionary")]
        public string? JsonDictString { get; set; }
    }
   ```

1. **Add your JSON field class to entity model as a property**

   ```cs
    public class Note
    {
        [Key, Required]
        public int Id { get; set; }
        public string? Header { get; set; }

        public JsonTodoItems Todos { get; set; } = new();
    }
   ```

   Or

   ```cs
    public class Product
    {
        [Key, Required]
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }

        public JsonParamsDictionary Parameters { get; set; } = new();
    }
   ```

1. **Usage example:**

   Here is example for `JsonEnumerableAdaptable`

   ```cs
    Note note = db.Notes.FirstOrDefault();
    note.Todos.Edit(en => en.Append(new("MyTodoItemTitle")));
   ```

   This will generate the following JSON data into the corresponding table string field:

   ```
   [ ... , { "Text": "MyTodoItemTitle", "CompleteStatus": false }]
   ```

   And here is example for `JsonDictionaryAdaptable`

   ```cs
    Product product = new() {Name="Phone",Price=500.95m,Amount=21,Parameters={
        VirtualDictionary = new Dictionary<string,object>() {
            {"Camera",13.5 },{"OS","Android" },{"Screen","1080x900"},{"Storage",32}
        }
    }};
    db.Goods.Add(product);
    db.SaveChanges();
   ```

   And here is also the result in JSON:

   ```
   { "Camera": 13.5, "OS": "Android", "Screen": "1080x900", "Storage": 32 }
   ```
