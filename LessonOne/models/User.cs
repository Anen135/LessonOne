

namespace LessonOne.models;

public class User
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string Email { get; set; } = "Unset";
    public UserAccess Access { get; set; } = UserAccess.Low;
    
}

public enum UserAccess
{
    Low, 
    Middle,
    High
}