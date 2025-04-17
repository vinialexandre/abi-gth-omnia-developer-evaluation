using System.Text.Json.Serialization;
using Abi.DeveloperEvaluation.Common.Validation;

public class ApiResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IEnumerable<ValidationErrorDetail>? Errors { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Data { get; set; }

    public static ApiResponse Ok(string message = "Operação realizada com sucesso.", object? data = null) =>
        new() { Success = true, Message = message, Data = data };

    public static ApiResponse Fail(string message, IEnumerable<ValidationErrorDetail>? errors = null) =>
        new() { Success = false, Message = message, Errors = errors ?? [] };
}
