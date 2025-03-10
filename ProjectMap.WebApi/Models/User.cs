namespace ProjectMap.WebApi.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public ICollection<Environment2D> Environments { get; set; }
    }
}
