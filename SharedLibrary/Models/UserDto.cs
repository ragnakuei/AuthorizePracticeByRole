namespace SharedLibrary.Models
{
    public class UserDto
    {
        public int UserId { get; set; }

        public string Account { get; set; }

        public string Name { get; set; }

        public System.DateTime Created { get; set; }

        public string[] Roles { get; set; }
    }
}