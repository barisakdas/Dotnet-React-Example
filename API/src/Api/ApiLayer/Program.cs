var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add controllers and filters. Burada kendi yazdýðýmýz filter ý ekliyoruz.
builder.Services.AddControllers(options =>
    options.Filters.Add(new ValidateFilterAttribute()))
        .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<HotelDtoValidator>());

// api nin kendi filter ýný kapattýk. Çünkü biz endpointten kendi isteðimiz modeli almak istiyoruz.
builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(NotFoundFilter<>));   // Generic olduðu için typeof ile kullanýyoruz.
builder.Services.AddAutoMapper(typeof(MapProfile));

builder.Services.AddScoped<IUnitOfWorks, UnitOfWorks>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));  // Generic olduðu için typeof ile kullanýyoruz.
builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));  // Generic olduðu için typeof ile kullanýyoruz.

// Kullanacaðýmýz db yi önce gidip appsettings.json içine ekledik.
builder.Services.AddDbContext<AppDbContext>(x =>
{
    // appsettings.json dan istediðim connection stringi alacaðýz.
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
    {
        // nerede kullanýlacaðýný tip güvenli þekilde belirtiyoruz. Direkt katmanýn ismini de yazabilirdik ama bu defa katman ismi deðiþince burayýda deðiþtirmek zorunda kalacaðýmýz için yapmýyoruz.
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

app.UseCustomException(); // Kendi yazdýðýmýz custom exception middleware.

app.UseAuthorization();

app.MapControllers();   // Bizim controller içinde yazdýðýmýz actionlarý yeni halindeki Map ler için dönüþtürecek.

app.Run();
