using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public interface IValidator<T>
    {
        void Validate(T entity);
    }
}
