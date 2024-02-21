using Google.Authenticator;
using MFA;
using MFA.Middleware;
using MFA.Validation.Interfaces;
using MFA.Validation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(q => new TwoFactorAuthenticator());
builder.Services.AddScoped<IApiKeyValidation, ApiKeyValidation>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.AddEndpointsAuthenticator();
app.UseMiddleware<ApiKeyMiddleware>();
app.Run();