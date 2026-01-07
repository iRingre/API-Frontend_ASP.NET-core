using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;

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
        private void newTodos(ChangeEventArgs args)
        {
            newTodo = (string)args.Value;
        }
        private void Invio(KeyboardEventArgs e)
        {
            bool control = true;
            foreach (var item in todos)
            {
                if (item.Title == newTodo)
                {
                    control = false;
                }
            }
            if (e.Code == "Enter" && control) todos.Add(new TodoItem { Title = newTodo });
        }
    }
}