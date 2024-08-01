using CleanArchitectureFront.Primitives;

namespace CleanArchitectureFront.Entities
{
    public class User : Entity
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
    }
}
