using api.Data;
using api.Interface;
using api.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddOpenApi();


builder.Services.AddDbContext<ApplicationDbContext>(options =>{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


//////////////DI////////////////////
///
//AddScope: her istek için yeni instance
//AddSingelton: Uygulamamız ilk çalıştığında ,servisin bir tane instance ’ını oluşturur ve bu bilgiyi memory de tutar. Servis her çağrıldığında en başta oluşturulan instance ’ı kullanılır.
//AddTransient: Servis her çağrıldığın da yeni bir instance oluşturur. Yani aynı istek aşamasında da farklı isteklerde de servis birden fazla kez çağrılıyorsa servis her çağrıldığında yeni bir instance oluşturur.

builder.Services.AddScoped<IStockRepository,StockRepository>();
builder.Services.AddScoped<ICommentRepository,CommentRepository>();

//AddScope: it defines lifetime

///////
///

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();



app.Run();

