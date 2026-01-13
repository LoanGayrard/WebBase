namespace Api.Dtos.Todo;

public record CreateTodoRequest
{
    public string Title { get; init; } = "";
}
