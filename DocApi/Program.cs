using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyWebAPI", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Name = "Authorization",
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference=new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{ }
                    }
                });
});

builder.Services.AddCors();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "http://localhost:5249",
                        ValidAudience = "http://localhost:5249",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SDMC-CJAS1-SAD-DFSFA-SADHJVF-VF1")),
                        ClockSkew = TimeSpan.FromHours(1)
                    };
                });
builder.Services.AddAuthorization();
var app = builder.Build();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapPost("/login", (string username) =>
{
    Claim[] claims = [new Claim("Username", username)];
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SDMC-CJAS1-SAD-DFSFA-SADHJVF-VF1"));
    var token = new JwtSecurityToken(
        "http://localhost:5249",
        "http://localhost:5249",
        claims, DateTime.Now, DateTime.Now.AddHours(1),
        new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );
    var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
    return jwtToken;
});
app.MapGet("/hello", () => Results.Ok(new Message() { Text = "Hello World!" })).RequireAuthorization(p => p.RequireClaim("username", "123"));
app.MapGet("/helloResult", () => TypedResults.Ok("Hello World!"));
app.MapGet("/hello/{name}", (string name) => $"Hello {name}!");

app.Run();


