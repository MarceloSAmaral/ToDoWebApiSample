using Microsoft.EntityFrameworkCore;
using ToDoApp.CoreObjects.Entities;

namespace ToDoApp.Data
{
    public class ToDoAppContext : DbContext
    {
        public ToDoAppContext() { }

        public ToDoAppContext(DbContextOptions<ToDoAppContext> options)
            : base(options)
        {
        }

        public DbSet<ToDoItem> ToDoItems { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasKey(u =>u.Id);
            modelBuilder.Entity<ToDoItem>().HasOne(item => item.User).WithMany().HasForeignKey(item => item.UserId);
        }
    }
}
