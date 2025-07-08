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

builder.Services.AddMemoryCache();
builder.Services.AddSingleton<Random>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("https://poker-trainer.netlify.app/")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontend");
app.UseHttpsRedirection();
app.MapControllers();

app.Run();