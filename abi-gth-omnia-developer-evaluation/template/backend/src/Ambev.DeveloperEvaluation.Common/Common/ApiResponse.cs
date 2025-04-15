using Ambev.DeveloperEvaluation.Common.Validation;

namespace Ambev.DeveloperEvaluation.WebApi.Common;

public class ApiResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public IEnumerable<ValidationErrorDetail> Errors { get; set; } = [];

    public static ApiResponse Ok(string message = "Operação realizada com sucesso.")
    {
        return new ApiResponse { Success = true, Message = message };
    }

    public static ApiResponse Fail(string message, IEnumerable<ValidationErrorDetail>? errors = null)
    {
        return new ApiResponse { Success = false, Message = message, Errors = errors ?? [] };
    }

}
