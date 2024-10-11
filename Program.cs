var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

Task<string> FetchFromDatabase()
{
    Task.Delay(3000).GetAwaiter().GetResult();
    return Task.FromResult("Data");
}

async Task<string> FetchFromDatabaseAsync()
{
    await Task.Delay(3000);
    return "Data";
}

app.MapGet("/sync", (ILogger<Program> _logger) =>
{
    _logger.LogInformation($"Thread counts: {ThreadPool.ThreadCount.ToString()}");
    string requestId = Guid.NewGuid().ToString();

    HttpClient client = new();
    string data = FetchFromDatabase().GetAwaiter().GetResult();

    return "Ok";
})
.WithName("RunSync")
.WithOpenApi();

app.MapGet("/async", async (ILogger<Program> _logger) =>
{
    _logger.LogInformation($"Thread counts: {ThreadPool.ThreadCount.ToString()}");
    string requestId = Guid.NewGuid().ToString();

    HttpClient client = new();
    string data = await FetchFromDatabaseAsync();

    return "Ok";
})
.WithName("RunAsync")
.WithOpenApi();

app.Run();
