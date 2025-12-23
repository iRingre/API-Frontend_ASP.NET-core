using Microsoft.AspNetCore.Components.Web;

namespace FrontEnd.Components.Pages
{
    public partial class Todo
    {

        List<TodoItem> todos = new List<TodoItem>();
        string newTodo = "";
        public class TodoItem
        {
            public string Title { get; set; }
            public bool IsDone { get; set; }

        }
        private void CreateTodo(MouseEventArgs args)
        {
            if (!string.IsNullOrEmpty(newTodo))
            {
                todos.Add(new TodoItem { Title = newTodo });
                newTodo = "";
            }

        }
    }
}