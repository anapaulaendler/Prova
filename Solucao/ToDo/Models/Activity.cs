public class Activity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string Name { get; set; }
    public string? Description { get; set; }
    public DateTime LimitDate { get; set; }
    public bool Status { get; set; }
}