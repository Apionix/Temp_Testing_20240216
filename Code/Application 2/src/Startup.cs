using Application2;
using Application2.Middleware;
using Microsoft.OpenApi.Models;
using System.Reflection;

// Add pre-defined response types for default API endpoints
[assembly: ApiConventionType(typeof(DefaultApiConventions))]

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

// Map secret from file to variable
builder.Services.AddSecret<Secret>("SECRET_NAME");

// Create MySQL configuration object
// TODO: Remove if MySQL connection is not required
builder.Services.AddSQL<Secret>(secret => secret.MySQLConnection);

// Define Authentication and Authorisation policies
// NB! Make use of attribute-based policies to enable granular policies
// TODO: Remove if Authentication and Authorisation is not needed
builder.Services.AddHttpContextAccessor();

if (builder.Environment.IsDevelopment())
{

    // Create swagger documentation
    // TODO: Update details according to project
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo()
        {
            Version = "v1",
            Title = "Template service",
            Description = "Testing API for the template repository"
        });

        // Include function xml comments in generated document
        string? xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });
}

// Add serialisation settings
builder.Services.AddControllers()
    .AddJsonOptions(option => option.JsonSerializerOptions.PropertyNamingPolicy = null);

// Create web application
WebApplication? app = builder.Build();

// Swagger configuration
if (app.Environment.IsDevelopment())
{

    // Configure the HTTP request pipeline.
    app.UseSwagger(options => options.SerializeAsV2 = true);
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;

        // Disable syntax highlighting to fix swagger hanging after request returns
        options.ConfigObject.AdditionalItems.Add("syntaxHighlight", false);
    });
}

app.UseRouting();

// Map controllers as API endpoints
app.UseEndpoints(endpoints => endpoints.MapControllers());

// Start application
app.Run();
