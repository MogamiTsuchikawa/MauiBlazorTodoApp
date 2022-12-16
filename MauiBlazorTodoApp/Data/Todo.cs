using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBlazorTodoApp.Data
{
    public class Todo
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool IsEnded { get; set; }
        public DateTime? Limit { get; set; }
        public Todo(string title, DateTime limit)
        {
            Id = Guid.NewGuid();
            Title = title;
            IsEnded = false;
            Limit = limit;
        }
        public Todo(string title)
        {
            Id = Guid.NewGuid();
            Title = title;
            IsEnded = false;
        }
        public Todo()
        {

        }
    }
}
