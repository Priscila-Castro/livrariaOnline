
using Microsoft.EntityFrameworkCore;
using LibraryOnline.Models;
using LibraryOnline.Books;

namespace LibraryOnline.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<books> books { get; set; }
}
