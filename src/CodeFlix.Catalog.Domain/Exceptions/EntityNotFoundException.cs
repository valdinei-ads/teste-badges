namespace CodeFlix.Catalog.Domain.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string? message) : base(message) { }
    }
}
