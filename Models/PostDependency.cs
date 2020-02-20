namespace ECAdmin.Models
{
    public class PostDependency
    {
        public int PostId { get; set; }
        public Post Post { get; set; }

        public int DependencyId { get; set; }
        public Dependency Dependency { get; set; }
    }
}