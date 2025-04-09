using Ucode.Api.Data;
using Ucode.Api.Handlers;
using Ucode.Core.Handlers;
using Microsoft.EntityFrameworkCore;
using Ucode.Api.Endpoints;




var builder = WebApplication.CreateBuilder(args);

// Configuraçao para acessar o Banco

var cnnStr = builder
    .Configuration
    .GetConnectionString("DefaultConnection") ?? string.Empty;

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(cnnStr);
});


// Configurações para o Swagger

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.CustomSchemaIds(n => n.FullName);
});


// Registra o handler como um serviço transiente
builder.Services.AddTransient<IStudentHandler,StudentHandler>();
builder.Services.AddTransient<ICourseHandler, CourseHandler>();
builder.Services.AddTransient<IGradeHandler, GradeHandler>();
builder.Services.AddTransient<IEnrollmentHandler, EnrollmentHandler>();



var app = builder.Build();

// Configura o uso do Swagger e a interface do Swagger UI
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API ControleAluno V1");
    c.RoutePrefix = string.Empty;
});

app.MapGet("/",() => new { message = "OK"});
app.MapEndpoints();

app.Run();






