var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => Results.Ok(new
    {
        mensaje = "API Semana14 funcionando correctamente",
        framework = ".NET 10",
        plataforma = "Render",
        estado = "Online"
    }))
    .WithTags("Estado");

app.Run();