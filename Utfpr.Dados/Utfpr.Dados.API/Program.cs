using Utfpr.Dados.API.Configurations;
using Utfpr.Dados.API.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.DependencyInjectionConfiguration();
builder.Services.AddProfilesConfiguration();
builder.Services.ConfigureDatabase(builder.Configuration, builder.Environment);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

await app.MigrateDatabase<ApplicationContext>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();