using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using HotelManagement.BusinessLogic.ILogic;
using HotelManagement.BusinessLogic.Logic;
using HotelManagement.DataAccess.Contexts;
using HotelManagement.DataAccess.IRepository;
using HotelManagement.DataAccess.Repository;
using HotelManagement.Models.Options;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using static Google.Apis.Drive.v3.DriveService;

var builder = WebApplication.CreateBuilder(args);

EmailOptions options = new();
builder.Configuration.GetSection(nameof(EmailOptions))
    .Bind(options);

builder.Services.AddDbContext<HotelManagementContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("YourConnectionStringName"));
});

// Add Swagger services
builder.Services.AddSwaggerGen(c =>
{
    c.MapType<HotelManagement.Models.DataModels.Role>(() => new OpenApiSchema { Type = "string" });
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "HotelManagement", Version = "v1" });
});


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IHotelRepository, HotelRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

builder.Services.AddScoped<IUserLogic, UserLogic>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IFileStorageService, GoogleDriveImageUploaderService>();
builder.Services.AddScoped<IRoleLogic, RoleLogic>();
builder.Services.AddScoped<IHotelLogic, HotelLogic>();
builder.Services.AddScoped<IRoomLogic, RoomLogic>();
builder.Services.AddScoped<IBookingLogic, BookingLogic>();

builder.Services.AddSingleton<DriveService>(new DriveService(new BaseClientService.Initializer()
{
    HttpClientInitializer = GoogleCredential.FromFile("credentials.json").CreateScoped(ScopeConstants.DriveFile),
    ApplicationName = "HMS Web Client"
}));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.HttpOnly = true;
        options.Cookie.Name = "LoginCookie";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(120);
        options.SlidingExpiration = true;
        options.LoginPath = new PathString("/Authentication/Login");
        options.Cookie.IsEssential = true;
        options.AccessDeniedPath = new PathString("/Error/Unauthorized");
    });

builder.Services.Configure<EmailOptions>(builder.Configuration.GetSection(nameof(EmailOptions)));
builder.Services.Configure<ProfilePictureOptions>(builder.Configuration.GetSection(nameof(ProfilePictureOptions)));
builder.Services.Configure<GoogleDriveOptions>(builder.Configuration.GetSection(GoogleDriveOptions.Options));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/User/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSwagger();


app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API Name V1");
    c.RoutePrefix = "swagger"; // You can change this to your desired URL path.
});

app.UseAuthentication();
app.UseAuthorization();

app.UseStatusCodePagesWithReExecute("/Error/NotFound");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Authentication}/{action=Login}/{id?}");

app.Run();
