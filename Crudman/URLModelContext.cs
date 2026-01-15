using Microsoft.EntityFrameworkCore;

public class URLModelContext(DbContextOptions<URLModelContext> options) : DbContext(options)
{
    public DbSet<Crudman.Models.URLModel> URLModel { get; set; } = default!;
}
