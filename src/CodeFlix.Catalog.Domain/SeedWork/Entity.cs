namespace CodeFlix.Catalog.Domain.SeedWork
{
    public abstract class Entity
    {
        public string Id { get; protected set; }

        protected Entity() => Id = Guid.NewGuid().ToString();

    }
}
