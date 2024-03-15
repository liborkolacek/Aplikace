using Aplikace.Models;
using Microsoft.EntityFrameworkCore;

namespace Aplikace.Databaze
{
    public interface IDbContext
    {
        DbSet<Pot> Pot { get; set; }
        DbSet<Lid> Lid { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
