using Microsoft.EntityFrameworkCore;
using ShirtsManagament.API.Data;
using ShirtsManagament.API.Repositories;
using ShirtsManagament.API.Repositories.IRepositories;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

//Services
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options => options
.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

WebApplication app = builder.Build();

//Pipeline
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
