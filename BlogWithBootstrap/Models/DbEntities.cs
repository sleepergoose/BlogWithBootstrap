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
        public DateTime AddingDate { get; set; }
        public string Category { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public List<Comment> Comments { get; set; }

        public Post()
        {
            Comments = new List<Comment>();
        }
    }

    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public List<Post> Posts { get; set; }
        public List<Comment> Comments { get; set; }
        public User()
        {
            Posts = new List<Post>();
            Comments = new List<Comment>();
        }
    }

    public class Comment
    {
        public int CommentId { get; set; }
        public string Text { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }
    }

    public struct Categories
    {
        public static string AspNetCore => "Asp Net Core"; 
        public static string EntityFramework => "Entity Framework"; 
        public static string AspNetMVC5 => "Asp Net MVC5"; 
        public static string MSSQL => "MS SQL"; 
        public static string Other => "Other"; 
        public static string CSharp => "C#"; 
        public static string DotNet => ".Net"; 
    }

    public struct Roles
    {
        public static string Admin => "admin";
        public static string Author => "author";
        public static string User => "user";
    }
}