using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiBlazorTodoApp.Data
{
    public class TodoService
    {
        private static TodoService _instance;
        public static TodoService Instance 
        { 
            get
            {
                if(_instance == null) 
                    _instance = new TodoService();
                return _instance;
            } 
        }

        public List<Todo> TodoList = new();
        public TodoService()
        {
            string filePath = Path.Combine(
                FileSystem.Current.AppDataDirectory, 
                "todolist.json");
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                TodoList = JsonSerializer.Deserialize<List<Todo>>(json);
            }
        }

        public async Task Commit()
        {
            string filePath = Path.Combine(
                FileSystem.Current.AppDataDirectory,
                "todolist.json");
            string json = JsonSerializer.Serialize(TodoList);
            await File.WriteAllTextAsync(filePath, json);
        }
    }
}
