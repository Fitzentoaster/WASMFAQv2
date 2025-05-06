using WASMFAQv2.Shared.Models;
using Microsoft.EntityFrameworkCore;
using WASMFAQv2.Resources;

namespace WASMFAQv2.Server.Contexts
{
    public class FAQContext : DbContext
    {
        public FAQContext(DbContextOptions<FAQContext> options) : base(options)
        {
        }
        public DbSet<QnASet> QnASets { get; set; }
        public DbSet<QnA> QnAs { get; set; }
        public DbSet<FAQ> FAQs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QnASet>()
                .HasMany(q => q.QnAs)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FAQ>().HasData(
                new FAQ
                {
                    FAQId = 1,
                    Title = AppStrings.FAQDefaultTitle,
                    Description = AppStrings.FAQDefaultDescription
                }
            );
        }
    }
}
