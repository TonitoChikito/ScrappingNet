using PruebaTecnica.Models;
using Microsoft.EntityFrameworkCore;

namespace PruebaTecnica.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
        public DbSet<Noticia> Noticias { get; set; } = null!;
    }
}