using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ShirtsManagament.API.Data;
using ShirtsManagament.API.Filters;
using ShirtsManagament.API.Repositories;
using ShirtsManagament.API.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc.Versioning;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

//Services
builder.Services.AddControllers();
builder.Services.AddApiVersioning(options => 
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1,0);
    options.ReportApiVersions = true;
});
builder.Services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'V");
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Finlandia", Version = "40%" , 
        Description = "**Найкраща г...** Нордична країна у Північній Європі. Межує зі Швецією на заході, Норвегією на північному заході та Росією на сході. На півдні й заході береги країни омивають води Балтійського моря та його заток — Фінської і Ботнічної. Столиця і найбільше місто — Гельсінкі (Хєльсінкі) ." });
    options.SwaggerDoc("v2", new OpenApiInfo { Title = "My web api v2", Version = "ver1" });
    options.OperationFilter<AuthorizationHeaderOperationFilter>();
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme 
    {
        Scheme = "Bearer",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        In = ParameterLocation.Header
    });
});

builder.Services.AddDbContext<ApplicationDbContext>(options => options
.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

WebApplication app = builder.Build();
if (app.Environment.IsDevelopment()) 
{
    app.UseSwagger();
    app.UseSwaggerUI(options => 
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "CannabisPlantations WebApi v1");
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "CannabisPlantations WebApi v2");
    });
}
//Pipeline

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
