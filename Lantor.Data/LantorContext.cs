using Lantor.DomainModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections;
using System.Net.Quic;

namespace Lantor.Data
{
    public class LantorContext: DbContext
    {
        public LantorContext(DbContextOptions<LantorContext> options) : base(options) { }
        public DbSet<LanguageSample> LanguageSamples { get; set; }
        public DbSet<MultilingualSample> MultilingualSamples { get; set; }
        public DbSet<LanguageVectorCache> LanguageVectorCache { get; set; }
        public DbSet<Alphabet> Alphabets { get; set; }
        public DbSet<User> Users { get; set; }

        private User _currentUser = User.GetNullUser();
        public User CurrentUser { 
            get { return _currentUser; }
            set { _currentUser = value; }
        }

        public LantorContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("lantor");
            modelBuilder.Entity<LanguageVectorCache>(c =>
            {
                c.OwnsOne(e => e.Vector, b =>
                {
                    b.Property(v => v.Data).HasConversion(
                        bitArray => ConvertToBytes(bitArray),
                        bytes => ConvertToBitArray(bytes)
                    );
                });
            });

            modelBuilder.Entity<Alphabet>(a =>
            {
                a.OwnsMany(e => e.LetterVectors, (OwnedNavigationBuilder<Alphabet, LetterVector> b) =>
                {
                    b.OwnsOne(e => e.Vector, (OwnedNavigationBuilder<LetterVector, HiDimBipolarVector> c) =>
                    {
                        c.Property(v => v.Data).HasConversion(
                            bitArray => ConvertToBytes(bitArray),
                            bytes => ConvertToBitArray(bytes)
                        );
                    });
                });
            });
            modelBuilder.Entity<Alphabet>().Navigation(a => a.LetterVectors).AutoInclude(false);

            var nullUser = User.GetNullUser();
            modelBuilder.Entity<Alphabet>().HasQueryFilter(abc => abc.OwnerId == CurrentUser.Id || abc.OwnerId == nullUser.Id);
            modelBuilder.Entity<MultilingualSample>().HasQueryFilter(abc => abc.OwnerId == CurrentUser.Id || abc.OwnerId == nullUser.Id);
            // modelBuilder.Entity<Alphabet>().Property(abc => abc.OwnerId). value should be > 0
        }

        private static byte[] ConvertToBytes(BitArray bitArray)
        {
            var bytes = new byte[bitArray.Length / 8];
            bitArray.CopyTo(bytes, 0);
            return bytes;
        }

        private static BitArray ConvertToBitArray(byte[] bytes)
        {
            return new BitArray(bytes);
        }

    }
}
