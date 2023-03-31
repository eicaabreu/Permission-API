using Microsoft.EntityFrameworkCore;

namespace Permission.Core.Models
{
    public class PaginatedList<T>
    {
        public IEnumerable<T> Items { get; }
        public int RecordsFiltered { get; }
        public int RecordsTotal { get; }

        public PaginatedList(IEnumerable<T> items, int recordsFiltered, int recordsTotal)
        {
            RecordsFiltered = recordsFiltered;
            RecordsTotal = recordsTotal;
            Items = items;
        }

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, IPaginationRequest paginatedRequest)
        {
            var count = await source.CountAsync();
            var items = await source
                .Skip(paginatedRequest.Start)
                .Take(paginatedRequest.Length)
                .ToListAsync();

            return new PaginatedList<T>(items, items.Count, count);
        }
    }
}
