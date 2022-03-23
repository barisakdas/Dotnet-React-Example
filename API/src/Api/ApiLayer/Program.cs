var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add controllers and filters. Burada kendi yazd���m�z filter � ekliyoruz.
builder.Services.AddControllers(options =>
    options.Filters.Add(new ValidateFilterAttribute()))
        .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<HotelDtoValidator>());

// api nin kendi filter �n� kapatt�k. ��nk� biz endpointten kendi iste�imiz modeli almak istiyoruz.
builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(NotFoundFilter<>));   // Generic oldu�u i�in typeof ile kullan�yoruz.
builder.Services.AddAutoMapper(typeof(MapProfile));

builder.Services.AddScoped<IUnitOfWorks, UnitOfWorks>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));  // Generic oldu�u i�in typeof ile kullan�yoruz.
builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));  // Generic oldu�u i�in typeof ile kullan�yoruz.

// Kullanaca��m�z db yi �nce gidip appsettings.json i�ine ekledik.
builder.Services.AddDbContext<AppDbContext>(x =>
{
    // appsettings.json dan istedi�im connection stringi alaca��z.
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
    {
        // nerede kullan�laca��n� tip g�venli �ekilde belirtiyoruz. Direkt katman�n ismini de yazabilirdik ama bu defa katman ismi de�i�ince buray�da de�i�tirmek zorunda kalaca��m�z i�in yapm�yoruz.
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });
}
);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCustomException(); // Kendi yazd���m�z custom exception middleware.

app.UseAuthorization();

app.MapControllers();   // Bizim controller i�inde yazd���m�z actionlar� yeni halindeki Map ler i�in d�n��t�recek.

app.Run();
