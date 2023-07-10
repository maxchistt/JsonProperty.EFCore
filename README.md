# JsonPropertyAdapter

## About

This project allows you to use JSON fields in EF Core without setting up Fluent API

## Instruction

Here are few steps how to use JsonPropertyAdapter project:

1. **Connect the project and specify 'using'**

   ```cs
   using JsonPropertyAdapter;
   ```

1. **Create Note entity model**

   ```cs
   public class Note
       {
           [Key, Required]
           public int Id { get; set; }
           public string? Header { get; set; }
       }
   ```

1. **Create a JSON list item class**

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
       public class TodoItems : JsonEnumerableAdaptable<TodoItem>
       {
           [Column("JsonTodoItemsList")]
           public string? JsonListString { get; set; }
       }
   ```

1. **Add your JSON list item class to Note entity model as a property**

   ```cs
   public class Note
       {
           [Key, Required]
           public int Id { get; set; }
           public string? Header { get; set; }

           public TodoItems Todos { get; set; } = new();
       }
   ```

1. **Usage example:**

   ```cs
   Note note = db.Notes.FirstOrDefault();
   note.Todos.Edit(en => en.Append(new("MyTodoItemTitle")));
   ```

   This will generate the following JSON data into the corresponding table string field:

   ```json
   [ ... , { "Text": "MyTodoItemTitle", "CompleteStatus": false }]
   ```
