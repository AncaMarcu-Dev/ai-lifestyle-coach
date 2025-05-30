using LifestyleCoach.Data;
using LifestyleCoach.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Register DbContext with SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=coach.db"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register HTTP client for calling Ollama
builder.Services.AddHttpClient<OllamaService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:11434");
});
builder.Services.AddScoped<JournalService>();
builder.Services.AddScoped<GoalService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
