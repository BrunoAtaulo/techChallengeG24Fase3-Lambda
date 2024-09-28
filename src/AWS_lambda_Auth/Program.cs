using AWS_lambda_Auth.Middleware;
using AWS_lambda_Auth.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add AWS Lambda support.
builder.Services.AddAWSLambdaHosting(LambdaEventSource.RestApi);

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
// Needed for minimal APIs

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(
        "v1",
        new OpenApiInfo

        {
            Title = "Lambda FIAP03 serverless.template -- https://docs.aws.amazon.com/pt_br/lambda/latest/dg/csharp-package-asp.html", 
            Version = "v1",
            Description = "API for FIAP Lambda Auth Fase 03- Gerenciamento de Login e Token.",  // Add a comprehensive description
            Contact = new OpenApiContact
            {
                Name = "G24",  // Replace with your name or team
            },
            License = new OpenApiLicense
            {
                Name = "MIT License",  // Replace with your desired license
                Url = new Uri("https://opensource.org/licenses/MIT")  // Link to the license
            }
        });

    // Enable annotations for automatic documentation generation
    c.EnableAnnotations();
});



//IOC

builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
// Enable Swagger in development mode
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    string swaggerJsonbasePatch = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
    c.SwaggerEndpoint($"{swaggerJsonbasePatch}/swagger/v1/swagger.json", "Lambda V1");
    c.OAuthAppName("FIAP Lambda auth Fase 03");
    c.OAuthScopeSeparator(" ");
    c.OAuthUsePkce();
});
//}

app.UseMiddleware<ExceptionMiddleware>();


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

//app.MapGet("/", () => "Bem vindo ao Lambda Auth Lanches BD Fiap");


app.Run();
