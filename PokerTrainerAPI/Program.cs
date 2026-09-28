using System.Text.Json.Serialization;
using Microsoft.OpenApi;
using PokerTrainerApi.DrawRanges;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IRangeRepository, RangeRepository>(provider =>
{
    var env = provider.GetRequiredService<IWebHostEnvironment>();
    var basePath = Path.Combine(env.ContentRootPath, "Ranges");

    var files = new Dictionary<PokerPosition, string>()
    {
        { PokerPosition.LJ, Path.Combine(basePath, "lowjack.json") },
        { PokerPosition.HJ, Path.Combine(basePath, "hijack.json") },
        { PokerPosition.CO, Path.Combine(basePath, "cutoff.json") },
        { PokerPosition.BTN, Path.Combine(basePath, "button.json") },
        { PokerPosition.SB, Path.Combine(basePath, "smallblind.json") },
    };

    return new RangeRepository(files);
});

builder.Services.AddSingleton<IDrawRangesService, DrawRangesService>();

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

var app = builder.Build();

app.MapOpenApi("/api/openapi/{documentName}.json");
app.UseSwaggerUI(options =>
{
    options.RoutePrefix = "api/swagger";
    options.SwaggerEndpoint("/api/openapi/v1.json", "v1");
});

app.MapControllers();

app.Run();