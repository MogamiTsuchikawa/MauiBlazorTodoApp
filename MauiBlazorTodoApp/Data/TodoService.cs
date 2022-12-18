using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Alerts;

namespace MauiBlazorTodoApp.Data
{
    public class TodoService
    {
        private static TodoService _instance;
        public static TodoService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TodoService();
                return _instance;
            }
        }

        public ObservableCollection<Todo> TodoList = new();
        public TodoService()
        {
            string filePath = Path.Combine(
                FileSystem.Current.AppDataDirectory,
                "todolist.json");
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                TodoList = new(JsonSerializer.Deserialize<List<Todo>>(json));
                DateTime lastTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
                TodoList.Where(t => 
                    t.Limit is null ? 
                    false : 
                    ((DateTime)t.Limit).CompareTo(lastTime) == -1 && 
                    ((DateTime)t.Limit).CompareTo(lastTime.AddDays(-1)) == 1).ToList().ForEach(t =>
                    {
                        Toast.Make($"本日期限:{t.Title}", CommunityToolkit.Maui.Core.ToastDuration.Long,14).Show();
                    });
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
