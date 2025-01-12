using CreatePDF.API.Services;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Set the QuestPDF license
QuestPDF.Settings.License = LicenseType.Community; // Use 'LicenseType.Commercial' if you have a commercial license


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<PdfService>();  // Adding Pdf Creation Service

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
