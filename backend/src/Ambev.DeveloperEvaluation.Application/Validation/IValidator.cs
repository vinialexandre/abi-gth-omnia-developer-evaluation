namespace Ambev.DeveloperEvaluation.Application.Validation
{
    public interface IValidator<T>
    {
        void Validate(T entity);
    }
}
