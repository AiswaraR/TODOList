using Common;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    internal class ToDoListDBContext : DbContext
    {
        public ToDoListDBContext(DbContextOptions<ToDoListDBContext> options) : base(options)
        {

        }

        public DbSet<ToDoList> ToDoListEntities { get; set; }
    }
}