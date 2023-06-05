//using Member.data.Interface;
//using Member.data.Repository;
//using Microsoft.AspNetCore.Authentication.JwtBearer;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
////builder.Services.AddSwaggerGen();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
//});

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

//}).AddJwtBearer(o =>
//{
//    o.Authority = builder.Configuration.GetValue<string>("Jwt:Authority");
//    o.Audience = builder.Configuration.GetValue<string>("Jwt:Audience");
//    o.RequireHttpsMetadata = false;

//    o.Events = new JwtBearerEvents()
//    {
//        OnAuthenticationFailed = c =>
//        {
//            c.NoResult();

//            c.Response.StatusCode = 500;
//            c.Response.ContentType = "text/plain";

//            if (builder.Environment.IsDevelopment())
//            {
//                return c.Response.WriteAsync(c.Exception.ToString());
//            }

//            return c.Response.WriteAsync("An error occured processing your authentication.");
//        }
//    };
//});
//var app = builder.Build();
//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();







using Keycloak.AuthServices.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Keycloak.AuthServices.Authorization;

var builder = WebApplication.CreateBuilder(args);

var host = builder.Host;
var configuration = builder.Configuration;
var services = builder.Services;

var authenticationOptions = configuration
    .GetSection(KeycloakAuthenticationOptions.Section)
    .Get<KeycloakAuthenticationOptions>();

services.AddKeycloakAuthentication(authenticationOptions);

services.AddAuthorization(options =>
{
    options.AddPolicy("RequireWorkspaces", builder =>
    {
        builder.RequireProtectedResource("workspaces", "workspaces:read") // HTTP request to Keycloak to check protected resource
            .RequireRealmRoles("User") // Realm role is fetched from token
            .RequireResourceRoles("Admin"); // Resource/Client role is fetched from token
    });
})
    .AddKeycloakAuthorization(configuration);

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "Hello World!");

app.Run();

