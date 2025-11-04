var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/not-so-secret/from-disk", async () =>
    {
        const string secretFilePath = "/mnt/secrets-store/not-secret.txt";
        
        if (!File.Exists(secretFilePath))
        {
            return Results.NotFound("Secret file not found on disk.");
        }
        var secret = await File.ReadAllTextAsync(secretFilePath);
        var notSoSecretSecret = new NotSoSecretSecret("disk", secret);
        return Results.Ok(notSoSecretSecret);
    })
    .WithName("get-not-so-secret-secret-from-disk");

app.MapGet("/not-so-secret/from-environment", () =>
    {
        var secret = Environment.GetEnvironmentVariable("NOT_SO_SECRET_SECRET");
        if (string.IsNullOrEmpty(secret))
        {
            return Results.NotFound("Environment variable 'NOT_SO_SECRET_SECRET' not found.");
        }
        var notSoSecretSecret = new NotSoSecretSecret("environment", secret);
        return Results.Ok(notSoSecretSecret);
    })
    .WithName("get-not-so-secret-secret-from-environment");


app.Run();

record NotSoSecretSecret(string Source, string NotSecret)
{
}