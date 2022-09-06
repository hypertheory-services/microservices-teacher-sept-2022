using System.Security.Claims;
using Dapr.Client;
using HypertheoryMessages.Training;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MongoDB.Bson.Serialization.Conventions;
using WebPresenceBFF;
using WebPresenceBFF.Adapters;
using WebPresenceBFF.Domain;
using WebPresenceBFF.Hubs;
using Course = HypertheoryMessages.Training.Course;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();
builder.Services.AddDaprClient();
var conventionPack = new ConventionPack()
{
    new CamelCaseElementNameConvention(),
    new IgnoreExtraElementsConvention(true),
    new EnumRepresentationConvention(MongoDB.Bson.BsonType.String)
};

ConventionRegistry.Register("Default", conventionPack, t => true);
// Add services to the container.
builder.Services.AddScoped<CourseCatalog>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(pol =>
    {
        pol.AllowAnyHeader();
        pol.AllowAnyMethod();
        pol.WithOrigins("http://localhost:4200");
        pol.AllowCredentials();

    });
});
var mongoConnectionString = builder.Configuration.GetConnectionString("mongodb");
builder.Services.AddSingleton<MongoDbBffAdapter>(sp => new MongoDbBffAdapter(mongoConnectionString));

builder.Services.AddMongoJwtAuth(builder, mongoConnectionString);
builder.Services.AddScoped<DaprAdapter>();

var app = builder.Build();
app.UseCloudEvents();
app.UseCors();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints => endpoints.MapSubscribeHandler());



// Configure the HTTP request pipeline.

app.MapGet("/courses", async (CourseCatalog catalog) =>
{
    var response = await catalog.GetCoursesAsync();
    return Results.Ok(response);

}).RequireAuthorization();
app.MapGet("/my/registrations", async (CourseCatalog catalog, ClaimsPrincipal cp) =>
{
    var user = cp.Identity!.Name!;
    return Results.Ok(await catalog.GetRegistrationsForUserAsync(user));
}).RequireAuthorization();
app.MapPost("/training/courses", async ([FromBody] Course course, CourseCatalog catalog) =>
{
    Console.WriteLine("Got a course " + course.CourseId);
    await catalog.AddCourseAsync(course);
    return Results.Ok();

}).WithTopic("webpresence", Course.Topic);

app.MapPost("/training/offerings", async ([FromBody] Offering offering, CourseCatalog catalog) =>
{
    Console.WriteLine("Got an offering for " + offering.CourseId);
    await catalog.AddCourseOfferingAsync(offering);
    return Results.Ok();

}).WithTopic("webpresence", Offering.Topic);


app.MapPost("/register", (Delegate)Auth.Register());

app.MapPost("/login", (Delegate)Auth.Login());

app.MapHub<CoursesHub>("/courses-hub", options =>
{
   
});
app.Run();



