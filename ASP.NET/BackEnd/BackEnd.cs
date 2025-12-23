using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Rewrite;


var builder = WebApplication.CreateBuilder(args);

//aggiungendo il singleton ho la stessa logica per le varie richieste ma ho un implementazine molto più fluida (database)
builder.Services.AddSingleton<ITaskService>(new InMemoryTaskService());

var app = builder.Build();
//reindirizza le chiamate con tasks/{id} a chiamate todos/{id}
app.UseRewriter(new RewriteOptions().AddRedirect("tasks/(.*)","todos/$1"));


//intercetta ogni richiesta e stampa [tipo, path, data_ora] per quando inizia e finisce
app.Use(async (context, next)=>
{
   Console.WriteLine($"[{context.Request.Method} {context.Request.Path} {DateTime.UtcNow}] Started");
   await next(context);
   Console.WriteLine($"[{context.Request.Method} {context.Request.Path} {DateTime.UtcNow}] Finished");
});

//lista dei todo
var todos = new List<Todo>();

//richiesta get che restituisce tutti i valori contenuti nella lista todo
//app.MapGet("/todos", ()=>todos);
app.MapGet("/todos", (ITaskService service)=>service.GetTodos());

//richiesta con di un singolo todo tramite id
app.MapGet("/todos/{id}", Results<Ok<Todo>, NotFound> (int id, ITaskService service) =>
{
   //var targetTodo =  todos.SingleOrDefault(t => id == t.Id);
   var targetTodo =  service.GetTodoById(id);
   return targetTodo is null ? TypedResults.NotFound() : TypedResults.Ok(targetTodo);
});

//prende e inserisce un todo nella lista
app.MapPost("/todos", (Todo task, ITaskService service) =>
{
   //todos.Add(task);
   service.AddTodo(task);
   return TypedResults.Created($"/todos/{task.Id}",task);

}).AddEndpointFilter(async (context, next) => 
{
   //controlli inserimento dati
   var taskArgument = context.GetArgument<Todo>(0);
   Console.WriteLine($"{taskArgument.Id} - {taskArgument.DueDate}");
   var errors = new Dictionary<string, string[]>();

   if(taskArgument.DueDate < DateTime.UtcNow) errors.Add(nameof(Todo.DueDate), ["Non puoi avere una data di scadenza nel passato"]);
   if(taskArgument.IsCompleted) errors.Add(nameof(Todo.IsCompleted), ["Non puoi aggiungere un todo già completato"]);
   if(errors.Count>0)return Results.ValidationProblem(errors);

   return await next(context);
});

//cancellazzione di tutti i todo
app.MapDelete("/todos/{id}", (int id, ITaskService service) =>
{
    //todos.RemoveAll(t => id ==t.Id);
    service.DeleteTodoBody(id);
    return TypedResults.NoContent();
});

app.Run();
//MVC libreria consigliata da samu

//record per oggetti Todo
public record Todo(int Id, string Name, DateTime DueDate, bool IsCompleted);


//interfaccia per injection dependency
interface ITaskService
{
   Todo ? GetTodoById(int id);

   List<Todo> GetTodos();

   void DeleteTodoBody(int id);

   Todo AddTodo(Todo task);
}

