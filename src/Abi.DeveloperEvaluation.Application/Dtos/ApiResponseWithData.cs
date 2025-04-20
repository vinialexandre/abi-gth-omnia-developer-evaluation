using Abi.DeveloperEvaluation.Common.Validation;

namespace Abi.DeveloperEvaluation.Application.Dtos;

public class ApiResponseWithData<T> : ApiResponse
{
    public T? Data { get; set; }

    public static ApiResponseWithData<T> Ok(T data, string message = "Operação realizada com sucesso.")
    {
        return new ApiResponseWithData<T> { Success = true, Message = message, Data = data };
    }

    public static ApiResponseWithData<T> Fail(string message, IEnumerable<ValidationErrorDetail>? errors = null)
    {
        return new ApiResponseWithData<T> { Success = false, Message = message, Errors = errors ?? [] };
    }
}
