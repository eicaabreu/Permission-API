namespace Permission.Core.Models
{
    public interface IPaginationRequest
    {
        int Start { get; }
        int Length { get; }
    }
}
