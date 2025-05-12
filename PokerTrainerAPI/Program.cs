using PokerTrainerAPI.Helpers;
using PokerTrainerAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IRangeService>(provider =>
{
    var env = provider.GetRequiredService<IWebHostEnvironment>();
    var path = Path.Combine(env.ContentRootPath, "Ranges", "button_open.json");
    return new RangeService(path);
});

builder.Services.AddMemoryCache();
builder.Services.AddSingleton<Random>();
builder.Services.AddScoped<IPokerService, PokerService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
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