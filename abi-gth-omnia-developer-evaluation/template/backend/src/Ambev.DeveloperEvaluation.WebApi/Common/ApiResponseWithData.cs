namespace Ambev.DeveloperEvaluation.WebApi.Common;

public class ApiResponseWithData<T> : ApiResponse
{
    public T? Data { get; set; }
}
