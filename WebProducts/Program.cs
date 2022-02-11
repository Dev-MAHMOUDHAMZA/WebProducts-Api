using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using WebProducts.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(option => 
    option.UseSqlServer(builder.Configuration.GetConnectionString("ProductConnection")));

builder.Services.AddCors(options => options.AddPolicy("CorsPolicy", buil =>
  {
      buil.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
     
  }));
//builder.Services.AddSingleton<IAuthorizationHandler, MyHandler1>();
//// MyHandler2, ...

//builder.Services.AddSingleton<IAuthorizationHandler, MyHandlerN>();

//// Configure your policies
//builder.Services.AddAuthorization(options =>
//      options.AddPolicy("Something",
//      policy => policy.RequireClaim("Permission", "CanViewPage", "CanViewAnything")));

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
app.UseCors("CorsPolicy");
app.Run();
