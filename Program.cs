using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using AirBnb.Data;
using AirBnb.Repositories;
using AirBnb.Services;
using AirBnb.Options;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Versioning;
using System.Reflection;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Configure Kestrel and HTTPS
builder.WebHost.UseKestrel(options =>
{
    options.ListenAnyIP(7279, listenOptions =>
    {
        listenOptions.UseHttps(); // HTTPS port
    });
});

// Configure DbContext
builder.Services.AddDbContext<AirBnBDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add repositories and services
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddScoped<IReservationsService, ReservationsService>();
builder.Services.AddScoped<ILandlordRepository, LandlordRepository>();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowCloudBnb", builder =>
    {
        builder.WithOrigins("https://cloudbnb-df3c1.web.app")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

// Configure Swagger/OpenAPI
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "AirBnb API", Version = "v1" });
    options.SwaggerDoc("v2", new OpenApiInfo { Title = "AirBnb API", Version = "v2" });

    // Enable XML comments if needed
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

// Configure CORS, DbContext, etc. as needed

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AirBnb API v1");
        c.SwaggerEndpoint("/swagger/v2/swagger.json", "AirBnb API v2");
    });
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
        }
    });
}

app.UseHttpsRedirection();
app.UseCors(); // Add your CORS policy name if required
app.UseAuthorization();
app.MapControllers();
app.Run();