using Donantes.API.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddDbContext<BloodDonationDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();


if (app.Environment.IsDevelopment())


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();