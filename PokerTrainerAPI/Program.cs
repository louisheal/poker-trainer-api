using System.Text.Json.Serialization;
using PokerTrainerApi.DrawRanges;
using PokerTrainerAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<List<RangeService>>(provider =>
{
    var env = provider.GetRequiredService<IWebHostEnvironment>();
    var basePath = Path.Combine(env.ContentRootPath, "Ranges");

    var files = new[]
    {
        new {path="lowjack.json", label="UTG"},
        new {path="hijack.json", label="HJ"},
        new {path="cutoff.json", label="CO"},
        new {path="button.json", label="BTN"},
        new {path="smallblind.json", label="SB"},
    };

    return files.Select(file =>
    {
        var fullPath = Path.Combine(basePath, file.path);
        return new RangeService(fullPath, file.label);
    }).ToList();
});

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

builder.Services.AddMemoryCache();
builder.Services.AddSingleton<Random>();
builder.Services.AddSingleton<IDrawRangesService, DrawRangesService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddOpenApi();

var app = builder.Build();

app.MapOpenApi("/api/openapi/{documentName}.json");
app.UseSwaggerUI(options =>
{
    options.RoutePrefix = "api/swagger";
    options.SwaggerEndpoint("/api/openapi/v1.json", "v1");
});

app.UseHttpsRedirection();
app.MapControllers();

app.Run();