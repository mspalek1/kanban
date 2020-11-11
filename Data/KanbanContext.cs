using Kanban.Models;
using Microsoft.EntityFrameworkCore;

namespace Kanban.Data
{
    public class KanbanContext : DbContext
    {
        public KanbanContext(DbContextOptions<KanbanContext> options)
        : base(options)
        {
        }
        public DbSet<Person> People { get; set; }
        public DbSet<Issue> Issues { get; set; }
    }
}
