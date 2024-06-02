namespace RectatSystem.Models;

public class User
{
    public int Id { get; set; }
    public long ChatId { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTimeOffset LastInteraction { get; set; }
    public string Response { get; set; }
    public DateTimeOffset ResponseDate { get; set; }
}
