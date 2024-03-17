using Lantor.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace Lantor.Data
{
    public class LantorContext: DbContext
    {
        public LantorContext(DbContextOptions<LantorContext> options) : base(options) { }
        public DbSet<LanguageSample> LanguageSamples { get; set; }
        public DbSet<MultilingualSample> MultilingualSamples { get; set; }
    }
}
