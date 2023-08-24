using ExpenseTracker.DB.Model;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.DB
{
    public class ExpenseContext:DbContext
    {
        public ExpenseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ExpenseCategory> Categories { set; get; }
        public DbSet<ExpenseEntry> Entrys { set; get; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ExpenseEntry>()
                .HasOne(e => e.Category)          
                .WithMany()                     
                .HasForeignKey(e => e.CatID)      
                .OnDelete(DeleteBehavior.Restrict);  
        }
    }
}
