namespace Api.Dtos.Todo;
public record TodoResponse
{
    public int Id { get; init; }
    public string Title { get; init; } = "";
    public bool Done { get; init; }
}
