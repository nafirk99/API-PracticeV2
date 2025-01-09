var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Enable static file serving
app.UseStaticFiles(); // This line enables serving static files from 'wwwroot' Order: Ensure that UseStaticFiles() is called before other middleware that might interfere with static file serving.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
