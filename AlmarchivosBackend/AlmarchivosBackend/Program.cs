using Microsoft.EntityFrameworkCore;  // Importar Entity Framework Core
using AlmarchivosBackend.Models;  // Asegúrate de reemplazar 'TuNamespace' con el nombre de tu proyecto

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost3000",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000", "http://localhost:4200")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});
// Agregar la configuración de la cadena de conexión
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Registrar el DbContext con la cadena de conexión
builder.Services.AddDbContext<AlmarchivosContext>(options =>
    options.UseSqlServer(connectionString));  // Configura el uso de SQL Server

// Agregar controladores
builder.Services.AddControllers();

// Configurar Swagger para la documentación de la API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurar el pipeline de la solicitud HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}





// Configurar el middleware de CORS
app.UseCors("AllowLocalhost3000");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
