using Microsoft.EntityFrameworkCore;
using OfficeSeatReservation.Data;
using OfficeSeatReservation.Domain;
using OfficeSeatReservation.Services;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

ConfigurationManager configuration = builder.Configuration;

//builder.Services.AddDbContext<SeatsReservationContext>(options =>
  //options.UseNpgsql(
    //configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<SeatsReservationContext>(options =>
    options.UseInMemoryDatabase(databaseName: "seatsDB"));

builder.Services.AddScoped<SeatsServices, SeatsServices>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<SeatsReservationContext>();
    dbContext.Database.EnsureCreated();

    if (!dbContext.Seats.Any())
    {
        for (int i = 1; i <= 30; i++)
        {
            dbContext.Seats.Add(new Seat { IsAvailable = true, SeatNumber = $"seat-{i}" });
        }
    }
    dbContext.SaveChanges();
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
