using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KanbanApp.Models;

namespace KanbanApp.Data
{
    public class KanbanAppContext : DbContext
    {
        public KanbanAppContext (DbContextOptions<KanbanAppContext> options)
            : base(options)
        {
        }

        public DbSet<KanbanApp.Models.Board> Board { get; set; } = default!;
        public DbSet<KanbanApp.Models.Column> Column { get; set; } = default!;
        public DbSet<KanbanApp.Models.Issue> Issue { get; set; } = default!;
        public DbSet<KanbanApp.Models.IssueColumn> IssueColumn { get; set; } = default!;
        public DbSet<KanbanApp.Models.User> User { get; set; } = default!;
        public DbSet<KanbanApp.Models.UserBoard> UserBoard { get; set; } = default!;
    }
}
