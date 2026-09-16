using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using PokerTrainerApi.DrawRanges.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PokerDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("PokerDb"),
        ServerVersion.AutoDetect(
            builder.Configuration.GetConnectionString("PokerDb")
        )
    ));

builder.Services.AddScoped<IRangeRepository, RangeRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Servers = new List<OpenApiServer>
        {
            new() { Url = "/" }
        };

        return Task.CompletedTask;
    });
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.MapOpenApi("/api/openapi/{documentName}.json");
app.UseSwaggerUI(options =>
{
    options.RoutePrefix = "api/swagger";
    options.SwaggerEndpoint("/api/openapi/v1.json", "v1");
});

app.UseCors();
app.MapControllers();

app.Run();