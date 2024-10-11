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

app.MapGet("/sync", (ILogger<Program> _logger) =>
{
    string requestId = Guid.NewGuid().ToString();
    _logger.LogInformation($"Starting SYNC request {requestId} with threadId: {Thread.CurrentThread.ManagedThreadId}");

    HttpClient client = new();
    string data = client.GetStringAsync("https://google.com")
    .GetAwaiter()
    .GetResult();

    _logger.LogInformation($"Finishing SYNC request {requestId} with threadId: {Thread.CurrentThread.ManagedThreadId}");
    return "Ok";
})
.WithName("RunSync")
.WithOpenApi();

app.MapGet("/async", async (ILogger<Program> _logger) =>
{
    string requestId = Guid.NewGuid().ToString();
    _logger.LogInformation($"Starting ASYNC request {requestId} with threadId: {Thread.CurrentThread.ManagedThreadId}");

    HttpClient client = new();
    string data = await client.GetStringAsync("https://google.com");

    _logger.LogInformation($"Finishing ASYNC request {requestId} with threadId: {Thread.CurrentThread.ManagedThreadId}");
    return "Ok";
})
.WithName("RunAsync")
.WithOpenApi();

app.Run();
