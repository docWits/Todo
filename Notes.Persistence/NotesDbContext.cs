using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Interfases;
using Notes.Domain;
using Notes.Persistence.EntityTypeConfiguration;

namespace Notes.Persistence
{
    public class NotesDbContext : DbContext,INotesDbContext
    {
        public DbSet<Note> Notes { get; set; }

        public NotesDbContext (DbContextOptions<NotesDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new NotesConfiguration());
            base.OnModelCreating(builder);
        }

    }
}
