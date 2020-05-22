namespace SharedLibrary.Entities
{
    public class UserGroup
    {
        public int UserId { get; set; }

        public int GroupId { get; set; }

        public System.DateTime Created { get; set; }

        public virtual Group Group { get; set; }

        public virtual User User { get; set; }
    }
}