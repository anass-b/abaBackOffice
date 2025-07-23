using abaBackOffice.DataAccessLayer;
using abaBackOffice.Helpers;
using abaBackOffice.Infrastructure;
using abaBackOffice.Interfaces;
using abaBackOffice.Interfaces.Repositories;
using abaBackOffice.Interfaces.Services;
using abaBackOffice.Repositories;
using abaBackOffice.Services;
using Helpers.EncryptionHelper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add configuration
var configuration = builder.Configuration;

// Add DbContext
builder.Services.AddDbContext<AbaDbContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(AbaAutoMapperProfile));

// Register encryption options
builder.Services.Configure<EncryptionOptions>(
    configuration.GetSection("Encryption"));

// Register Services
builder.Services.AddScoped<IAbllsTaskService, AbllsTaskService>();
builder.Services.AddScoped<IAbllsVideoService, AbllsVideoService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IBlogCommentService, BlogCommentService>();
builder.Services.AddScoped<IBlogPostService, BlogPostService>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<IDocumentService, DocumentService>();
builder.Services.AddScoped<IEncryptionService, EncryptionService>();
builder.Services.AddScoped<IOtpCodeService, OtpCodeService>();
builder.Services.AddScoped<IReinforcementProgramService, ReinforcementProgramService>();
builder.Services.AddScoped<IReinforcerAgentService, ReinforcerAgentService>();
builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IVideoService, VideoService>();

// Register Repositories
builder.Services.AddScoped<IAbllsTaskRepository, AbllsTaskRepository>();
builder.Services.AddScoped<IAbllsVideoRepository, AbllsVideoRepository>();
builder.Services.AddScoped<IBlogCommentRepository, BlogCommentRepository>();
builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();
builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
builder.Services.AddScoped<IOtpCodeRepository, OtpCodeRepository>();
builder.Services.AddScoped<IReinforcementProgramRepository, ReinforcementProgramRepository>();
builder.Services.AddScoped<IReinforcerAgentRepository, ReinforcerAgentRepository>();
builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IVideoRepository, VideoRepository>();

// Register Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// JWT Authentication (optional � uncomment if needed)
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = true;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
        };
    });

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "ABA BackOffice API", Version = "v1" });
});

// Controllers
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
// CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularFrontend", policy =>
    {
        policy.allowAnyOrigin()        /*.WithOrigins("http://localhost:4200")*/
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // facultatif
    });
});

var app = builder.Build();

// Middlewares
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "videos", "thumbnails")),
    RequestPath = "/uploads/videos/thumbnails"
});

app.UseHttpsRedirection();
app.UseCors("AllowAngularFrontend");
app.UseAuthentication(); // Only if JWT enabled
app.UseAuthorization();

app.MapControllers();
void PrintEncryptedPassword()
{
    string plainPassword = "Admin@1234";
    string passphrase = "mY^s3cRe7Pa$$phr@s3!x_2025@";

    string encrypted = EncryptionHelper.EncryptAES(plainPassword, passphrase);
    Console.WriteLine($"Encrypted password: {encrypted}");
}

PrintEncryptedPassword();
app.Run();
