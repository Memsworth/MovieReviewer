namespace MovieReviewer.Core.Domain;

public class User : BaseEntity
{
    public string Username { get; set; }
    public DateTime DateOfBirth { get; set; }
}