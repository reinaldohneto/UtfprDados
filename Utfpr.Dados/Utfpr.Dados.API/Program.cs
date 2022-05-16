using Utfpr.Dados.API.Configurations;
using Utfpr.Dados.API.Configurations.Api;
using Utfpr.Dados.API.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(typeof(SyncFluentValidationFilter));
    opt.Filters.Add(typeof(AsyncFluentValidationFilter));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AdicionarAutenticacaoSwagger();
builder.Services.DependencyInjectionConfiguration();
builder.Services.AddProfilesConfiguration();
builder.Services.ConfigureDatabase(builder.Environment);
builder.Services.ConfigureMessageQueue();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

await app.MigrateDatabase<ApplicationContext>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers().RequireAuthorization();

app.Run();