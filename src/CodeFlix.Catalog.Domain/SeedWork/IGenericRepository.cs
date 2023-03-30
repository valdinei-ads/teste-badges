namespace CodeFlix.Catalog.Domain.SeedWork
{
    public interface IGenericRepository<TAggregate> : IRepository
    {
        ValueTask InsertAsync(TAggregate aggregate, CancellationToken cancellationToken);
        ValueTask<TAggregate> GetAsync(string id, CancellationToken cancellationToken);
    }
}
