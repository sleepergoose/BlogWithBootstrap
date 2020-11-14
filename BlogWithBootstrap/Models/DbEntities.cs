using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogWithBootstrap.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public string Comments { get; set; }
    }

    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
    }

    public class Comment
    {
        public int CommentId { get; set; }
        public string Text { get; set; }
        public User User { get; set; }
        public Post Post { get; set; }
    }

    public static class AppVariables
    {
        public static HashSet<string> Categories = new HashSet<string> { 
            "Asp Net Core",
            "Entity Framework",
            "Asp Net MVC 5",
            "MS SQL",
            "Other",
            "C#",
            ".Net"
        };
    }
}