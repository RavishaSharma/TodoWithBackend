using todobackend.models;
using todobackend.services;
using Microsoft.AspNetCore.Mvc;

namespace todobackend.controllers;

[ApiController]
[Route("api/[controller]")] // this way we name the controller as Todo for Todos - route will be /api/Todo
public class TodoController : ControllerBase
{
    private readonly TodoService todoService;
    public TodoController(TodoService service)
    {
        this.todoService = service;
    }

    [HttpGet]
    public async Task<List<Todo>> Get()
    {
        return await todoService.getTodo(); // returns the list of Todos
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Todo>> Get(string Id)
    {
        var todo = await todoService.getTodo(Id);
        if (todo is null)
        {
            return NotFound();
        }
        return todo;  // returns the Todo with the provided id
    }

    [HttpPost]
    public async Task<ActionResult> Post(Todo newTodo)
    {
        await todoService.createTodo(newTodo);
        return CreatedAtAction(nameof(Get), new { id = newTodo.id }, newTodo);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string Id)
    {
        var todo = await todoService.getTodo(Id);
        if (todo is null)
        {
            return NotFound();
        }
        await todoService.deleteTodo(todo.id);
        return NoContent(); // returns 204 no content which means it was successfully deleted
    }
}