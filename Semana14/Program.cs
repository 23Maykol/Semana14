var builder = WebApplication.CreateBuilder(args);

// Registrar servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Habilitar Swagger también en Render (Production)
app.UseSwagger();
app.UseSwaggerUI();

// Render maneja HTTPS mediante su proxy
if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => Results.Ok(new
    {
        mensaje = "API Semana14 funcionando",
        entorno = app.Environment.EnvironmentName,
        estado = "Online"
    }))
    .WithTags("Estado");

app.Run();