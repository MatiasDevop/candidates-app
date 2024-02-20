using Candidates.Backend.Api.Helpers;
using Candidates.Backend.Application.Candidates;
using Candidates.Backend.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( options => {
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Title Api",
        Description = "A simple description for api swagger iformation",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Your Name xyz",
            Email = "xyz@gmail.com",
            Url = new Uri("https://example.com/")
        },
        License = new OpenApiLicense
        {
            Name = "Use under OpenApiLicence",
            Url = new Uri("https://example.com/licence"),
        }
    });
    var filename = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
    var filepath = Path.Combine(AppContext.BaseDirectory, filename);
    options.IncludeXmlComments(filepath);
});

builder.Services.AddTransient<CandidatesInitializer>();
builder.Services.AddScoped<ICandidateService, CandiateService>();

builder.Services.AddAutoMapper(typeof(MappingProfiles));

builder.Services.AddDbContext<CandidateDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

if (args.Length == 1 && args[0].ToLower() == "seeddata")
    SeedData(app);

void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<CandidatesInitializer>();
        service.Seed();
    }
}

// Configure the HTTP request pipeline.
app.Logger.LogInformation("Current Environment is {enviromentName}", app.Environment.EnvironmentName);
app.Logger.LogInformation("Current settings is :{MySettings}", builder.Configuration.GetValue<string>("MySettings"));

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Environments V1"));
}
if (app.Environment.IsStaging())
{
    //app.Logger.LogInformation("Current Environment is {enviromentName}", app.Environment.EnvironmentName);
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Staging V1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
