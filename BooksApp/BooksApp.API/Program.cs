using BooksApp.API.Services;
using BooksApp.Infrastructure.DataAcces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
                .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("db");
//Eğer; lazy loadin tekniğini kullanmak isterseniz EF.Proxies paketini yükleyin ve UseLazyLoadingProxies() ext. metodunu kullanın
builder.Services.AddDbContext<BooksAppDbContext>(option => option.UseSqlServer(connectionString, opt =>
{
    //Eğer dbContext'in varsayılan davranışının; Query Splitting olmasını istersek:
    opt.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);

}));
builder.Services.AddScoped<QueryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
