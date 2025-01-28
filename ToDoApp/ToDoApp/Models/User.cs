namespace ToDoApp.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }

        public User(int userId, string? name, string? userName, string? password)
        {
            UserId = userId;
            Name = name;
            UserName = userName;
            Password = password;
        }
    }
}
