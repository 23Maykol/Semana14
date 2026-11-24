var builder = WebApplication.CreateBuilder(args);

// Servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger disponible también en Render
app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();

// Endpoint de prueba
app.MapGet("/", () => Results.Ok(new
    {
        mensaje = "Laboratorio 14 funcionando correctamente",
        framework = ".NET 10",
        plataforma = "Render",
        estado = "Online"
    }))
    .WithTags("Estado");

app.Run();