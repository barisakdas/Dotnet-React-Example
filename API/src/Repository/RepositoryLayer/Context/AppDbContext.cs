namespace RepositoryLayer.Context;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<HotelEntity> Hotels { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // Yukarıdaki ile aynı işi yapıyor
        //modelBuilder.ApplyConfiguration(new HotelConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}