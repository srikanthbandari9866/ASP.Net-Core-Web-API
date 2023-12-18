using CompressedStaticFiles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NZWalksAPI.Data;
using NZWalksAPI.Mappings;
using NZWalksAPI.Middlewares;
using NZWalksAPI.Repositories;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var logger = new LoggerConfiguration()
        .WriteTo.Console()
        .WriteTo.File("Logs/NZWalks_Log.txt",rollingInterval:RollingInterval.Day)
        //.MinimumLevel.Information()
        .MinimumLevel.Warning()
        .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

//builder.Services.AddControllers();
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("V1", new OpenApiInfo
    {
        Version = "V1",
        Title = "WebAPI",
        Description = "NZWalks WebAPI"
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});
builder.Services.AddAuthentication(opt => {
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
            ValidAudience = builder.Configuration["JWT:ValidAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
        };
    });
//builder.Services.AddSwaggerGen(options =>
//{
//    options.SwaggerDoc("v1", new OpenApiInfo { Title= "NZ Walks API", Version= "v1" });
//    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
//    {
//        Name = "Authorization",
//        In = ParameterLocation.Header,
//        Type = SecuritySchemeType.ApiKey,
//        Scheme = JwtBearerDefaults.AuthenticationScheme
//    });
//    options.AddSecurityRequirement(new OpenApiSecurityRequirement
//    {
//        new OpenApiSecurityScheme
//        {
//            Reference = new OpenApiReference
//            {
//                Type = ReferenceType.SecurityScheme,
//                Id = JwtBearerDefaults.AuthenticationScheme
//            },
//            Scheme = "Oauth2",
//            Name = JwtBearerDefaults.AuthenticationScheme,
//            In = ParameterLocation.Header
//        },
//        new List<string>()
//    });
//});
builder.Services.AddDbContext<NZWalksDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksConnectionString"))
);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
                                             builder.AllowAnyOrigin()
                                                    .AllowAnyMethod()
                                                    .AllowAnyHeader());
});

builder.Services.AddScoped<IRegionRepo,RegionRepo>();
builder.Services.AddScoped<IWalkRepo,WalkRepo>();
builder.Services.AddScoped<IUserRepo,UserRepo>();
builder.Services.AddScoped<IImageRepo,ImageRepo>();

//builder.Services.AddCompressedStaticFiles();

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/V1/swagger.json", "NZWalks WebAPI");
    });
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseCors("CorsPolicy");

app.UseDefaultFiles();
//app.UseCompressedStaticFiles();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"Images")),
    RequestPath = "/Images"
    //https://localhost:7157/images
});

app.MapControllers();

app.Run();
