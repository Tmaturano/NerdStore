namespace NS.Core.Data;

public interface IUnitOfWork
{
    Task<bool> CommitAsync();
}
