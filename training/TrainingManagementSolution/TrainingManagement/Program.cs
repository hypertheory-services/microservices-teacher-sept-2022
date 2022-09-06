using MongoDB.Driver;
using TrainingManagement.Adapters;
using TrainingManagement.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDaprClient();
var mongoConnectionString = builder.Configuration.GetConnectionString("mongodb");
builder.Services.AddSingleton<MongoDbTrainingAdapter>(sp =>
{
    return new MongoDbTrainingAdapter(mongoConnectionString);
});


builder.Services.AddScoped<CourseCatalog>();
builder.Services.AddScoped<InstructorManager>();
builder.Services.AddScoped<DaprAdapter>();



var app = builder.Build();




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
