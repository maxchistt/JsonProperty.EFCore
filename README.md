# JsonProperty.EFCore

[![Version](https://img.shields.io/nuget/vpre/JsonProperty.EFCore.svg)](https://www.nuget.org/packages/JsonProperty.EFCore)

## About

This project allows you to use JSON fields in EF Core without setting up Fluent API

| Id  | Name  | Price  | Params                                                             |
| --- | ----- | ------ | ------------------------------------------------------------------ |
| 1   | Phone | 500    | {"Camera":13.5,"OS":"Android 11","Screen":"1080x900","Storage":32} |
| 2   | Car   | 100000 | {"MaxSpeed":300,"Engine capacity":6,"ElectroCar":false}            |
| 3   | Bag   | 400    | {"Voliume":5,"Color":"Red"}                                        |

## Instruction

Here are few steps how to use JsonProperty.EFCore project:

1. **Connect the project and specify 'using'**

   ```cs
   using JsonProperty.EFCore;
   ```

1. **Create entity model**

   ```cs
   public class Product
   {
       [Key, Required]
       public int Id { get; set; }
       public string? Name { get; set; }
       public decimal? Price { get; set; }
   }
   ```

   Or

   ```cs
   public class Note
   {
       [Key, Required]
       public int Id { get; set; }
       public string? Header { get; set; }
   }
   ```

1. **Create a JSON collection item class if necessary**

   ```cs
   public class TodoItem
   {
       public string? Text { get; set; }
       public bool? CompleteStatus { get; set; }
   }
   ```

1. **Add JSON field property of type `JsonEnumerable`, `JsonList` or `JsonDictionary` to your entity model**

   You can use the basic type like `JsonEnumerable` or `JsonDictionary` which uses elements of type object

   ```cs
   public class Product
   {
       [Key, Required]
       public int Id { get; set; }
       public string? Name { get; set; }
       public decimal? Price { get; set; }

       public JsonDictionary Parameters { get; set; } = new();
   }
   ```

   Or add property of generic type like `JsonEnumerable<T>` or `JsonDictionary<TKey, TValue>` with custom element type

   ```cs
   public class Note
   {
       [Key, Required]
       public int Id { get; set; }
       public string? Header { get; set; }

       public JsonEnumerable<TodoItem> Todos { get; set; } = new();
   }
   ```

1. **Usage example:**

   Here is example for `JsonDictionary`

   ```cs
   Product product = new() {Name="Phone",Price=500.95m,Amount=21,Parameters={
       VirtualDictionary = new Dictionary<string,object>() {
           {"Camera",13.5 },{"OS","Android" },{"Screen","1080x900"},{"Storage",32}
       }
   }};
   db.Goods.Add(product);
   db.SaveChanges();
   ```

   This will generate the following JSON data into the corresponding table string field:

   ```
   { "Camera": 13.5, "OS": "Android", "Screen": "1080x900", "Storage": 32 }
   ```

   Or next if `JsonSettings.StrictTypeSerialization` is 'true' (default)

   ```json
   {
     "Camera": [13.5, "System.Double"],
     "OS": ["Android", "System.String"],
     "Screen": ["1080x900", "System.String"],
     "Storage": [32, "System.Int32"]
   }
   ```

   And here is example for `JsonEnumerable<T>`

   ```cs
   Note note = db.Notes.FirstOrDefault();
   note.Todos.Edit(en => en.Append(new("MyTodoItemTitle")));
   ```

   And here is also the result in JSON:

   ```
   [ ... , { "Text": "MyTodoItemTitle", "CompleteStatus": false }]
   ```

   Or

   ```json
   [
     {
       "Text": ["MyTodoItemTitle", "System.String"],
       "CompleteStatus": [false, "System.Boolean"]
     }
   ]
   ```
