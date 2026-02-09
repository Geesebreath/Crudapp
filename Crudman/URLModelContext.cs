using Microsoft.EntityFrameworkCore;

public class UrlModelContext(DbContextOptions<UrlModelContext> options) : DbContext(options)
{
    public DbSet<Crudman.Models.UrlModel> UrlModel { get; set; } = default!;
}
