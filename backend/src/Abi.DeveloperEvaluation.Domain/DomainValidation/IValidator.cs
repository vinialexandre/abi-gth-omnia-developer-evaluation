namespace Abi.DeveloperEvaluation.Domain.DomainValidation
{
    public interface IValidator<T>
    {
        void Validate(T entity);
    }
}
