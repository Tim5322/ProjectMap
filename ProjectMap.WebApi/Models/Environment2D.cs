namespace ProjectMap.WebApi.Models
{
    public class Environment2D
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Guid UserId { get; set; }
        public ICollection<Object2D>? Objects { get; set; }
    }
}
