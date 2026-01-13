using Api.Data;
using Api.Data.Entities;
using Api.Dtos.Todo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/todos")]
[Authorize]
public class TodoController : ControllerBase
{
    private readonly AppDbContext _db;

    public TodoController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<TodoResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<TodoResponse>>> GetAll()
    {
        var todos = await _db.Todos
            .AsNoTracking()
            .Select(t => new TodoResponse
            {
                Id = t.Id,
                Title = t.Title,
                Done = t.Done
            })
            .ToListAsync();

        return Ok(todos);
    }

    [HttpPost]
    [ProducesResponseType(typeof(TodoResponse), StatusCodes.Status201Created)]
    public async Task<ActionResult<TodoResponse>> Create([FromBody] CreateTodoRequest dto)
    {
        var todo = new Todo
        {
            Title = dto.Title,
            Done = false
        };

        _db.Todos.Add(todo);
        await _db.SaveChangesAsync();

        var response = new TodoResponse
        {
            Id = todo.Id,
            Title = todo.Title,
            Done = todo.Done
        };

        return CreatedAtAction(nameof(GetAll), new { id = todo.Id }, response);
    }
}
