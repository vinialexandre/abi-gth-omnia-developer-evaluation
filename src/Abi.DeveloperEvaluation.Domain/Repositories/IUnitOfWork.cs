namespace Abi.DeveloperEvaluation.Domain.Repositories;

public interface IUnitOfWork
{
    Task CommitAsync();
}
