namespace Permission.Core.Models
{
    public abstract record BasePaginatedRequest(
        int Start = 0,
        int Length = 10,
        string? Search = null
    ) : IPaginationRequest;
}
