using System.Collections.Immutable;
using System.Text;
using ApiTienda.Data;
using ApiTienda.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using TiendaAPI.Services;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

var MyOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyOrigins, policy => {
            policy.WithOrigins("*");
            policy.AllowAnyHeader();
        policy.WithMethods("GET", "POST", "PUT", "DELETE");
        });
});

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => 
{
    //autorizacion desde swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Autorizacion.",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    c.OperationFilter<SecurityRequirementsOperationFilter>();
});

//JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => 
{
    string jwtKey = builder.Configuration["Jwt:key"] ?? "";
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

//DbContext
builder.Services.AddSqlServer<ProtibleDbContext>(builder.Configuration.GetConnectionString("TiendaConnection"));

//Stripe
StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

//Servie layer
builder.Services.AddScoped<ProductoService>();
builder.Services.AddScoped<OfertaService>();
builder.Services.AddScoped<CategoriaService>();
builder.Services.AddScoped<VentaService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<PlataformaService>();
builder.Services.AddScoped<KeyService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())    
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
//cords
app.UseCors(MyOrigins);

//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
