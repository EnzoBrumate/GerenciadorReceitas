using GerenciadorReceitas.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpClient<MealDbService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Gerenciador de Receitas",
        Version = "v1",
        Description = "API para buscar receitas na TheMealDB e gerenciar receitas personalizadas",
        Contact = new OpenApiContact
        {
            Name = "Enzo Brumate",
        }
    });

    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gerenciador de Receitas v1");
        c.DocumentTitle = "Gerenciador de Receitas - Swagger";
    });
}

app.MapControllers();

app.Run();
