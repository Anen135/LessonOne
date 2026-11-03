using LessonOne.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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