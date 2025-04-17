namespace Abi.DeveloperEvaluation.Application.Dtos;

public class PaginatedResponse<T> : ApiResponse
{
    public List<T> Sales { get; set; } = [];
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }

    public static PaginatedResponse<T> Ok(List<T> items, int page, int totalPages, int totalCount)
    {
        return new PaginatedResponse<T>
        {
            Success = true,
            Message = "Operação realizada com sucesso.",
            Sales = items,
            CurrentPage = page,
            TotalPages = totalPages,
            TotalCount = totalCount
        };
    }
}
