using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using OfficeSeatReservation.Data;
using OfficeSeatReservation.Services;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

ConfigurationManager configuration = builder.Configuration;

builder.Services.AddDbContext<SeatsReservationContext>(options =>
    options.UseNpgsql(
        configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<SeatsServices, SeatsServices>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<SeatsReservationContext>();
    dbContext.Database.EnsureCreated();

    if (!dbContext.Seats.Any())
    {
        dbContext.Seats.Add(new OfficeSeatReservation.Domain.Seat { IsAvailable = true, SeatNumber = "1" });
        dbContext.Seats.Add(new OfficeSeatReservation.Domain.Seat { IsAvailable = true, SeatNumber = "2" });
        dbContext.Seats.Add(new OfficeSeatReservation.Domain.Seat { IsAvailable = true, SeatNumber = "3" });
        dbContext.Seats.Add(new OfficeSeatReservation.Domain.Seat { IsAvailable = true, SeatNumber = "4" });
        dbContext.Seats.Add(new OfficeSeatReservation.Domain.Seat { IsAvailable = true, SeatNumber = "5" });
    }
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
