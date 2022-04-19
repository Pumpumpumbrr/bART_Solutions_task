using bART_Solutions_task.Core.Services.Implementation;
using bART_Solutions_task.Core.Services.Interfaces;
using bART_Solutions_task.Data.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<IContactService, ContactService>();
builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<IIncidentService, IncidentService>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "bArt Solutions Test API");

});

app.UseRouting();

app.UseAuthorization();

app.UseStaticFiles();

app.Map("/", () => Results.Redirect("/swagger"));

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();


