using ChemistryProjectPrep.API.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Configure all services
builder.Services.AddApplicationServices();
builder.Services.AddCorsConfiguration();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddJwtAuthenticationService(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Database Configuration
DatabaseConfiguration.ConfigureDatabase(builder.Services, builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();