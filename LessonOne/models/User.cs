

namespace LessonOne.models;

public class User
{
    public int Id { get; set; }
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = "Unset";
    public UserAccess Access { get; set; } = UserAccess.Low;
    
}

public enum UserAccess
{
    Low, 
    Middle,
    High
}
