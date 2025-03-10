namespace ProjectMap.WebApi.Models
{
    public class Environment2D
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int MaxHeight { get; set; }
        public int MaxLength { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public ICollection<Object2D>? Objects { get; set; }
    }
}
