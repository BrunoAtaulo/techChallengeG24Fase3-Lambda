using AWS_lambda_Auth.Middleware;
using AWS_lambda_Auth.Services;

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
    c.EnableAnnotations();  // Habilitar o uso de anotações
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
