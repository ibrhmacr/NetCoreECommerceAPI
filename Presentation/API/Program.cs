using System.Security.Claims;
using System.Text;
using API.Configurations.ColumnWriter;
using API.Extensions;
using API.Filters;
using Application;
using Application.Validators;
using FluentValidation.AspNetCore;
using Infrastructure;
using Infrastructure.Filters;
using Infrastructure.Services.Storage.LocalStorage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Sinks.PostgreSQL;
using SignalR;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();// Client^tan gelen request dogrultusundan olusturulan HttpContext Nesnesine
//katmanlardaki class^lar uzerinden(business logic) erisebilmemizi saglayan servistir.
builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddStorage<LocalStorage>(); //Storage Stratejisi
builder.Services.AddApplicationServices();
builder.Services.AddSignalRServices();
//builder.Services.AddCors(options =>
//    options.AddDefaultPolicy(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials()));
//Hub Servisleri baglabti kurabilmesi icin Credientialsa izin vermeyi unutma.

Logger log = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt")
    .WriteTo.PostgreSQL(builder.Configuration.GetConnectionString("PostgreSQL"), "logs",
        needAutoCreateTable: true,
        columnOptions: new Dictionary<string, ColumnWriterBase>
        {
            { "message", new RenderedMessageColumnWriter() },
            { "message_template", new MessageTemplateColumnWriter() },
            { "level", new LevelColumnWriter() },
            { "time_stamp", new TimestampColumnWriter() },
            { "exception", new ExceptionColumnWriter() },
            { "log_event", new LogEventSerializedColumnWriter() },
            { "user_name", new UsernameColumnWriter() } // ColumnWriterBase de olmadigi icin veritabaninda kendimiz olusturmamiz gerekir.
        })
    .Enrich.FromLogContext()
    .MinimumLevel.Information()
    .CreateLogger();

builder.Host.UseSerilog(log);

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;

});


builder.Services.AddControllers(options =>
    {
        options.Filters.Add<ValidationFilter>();
        options.Filters.Add<RolePermissionFilter>();
    })
    .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<IValidator>())
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin",options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            
            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
            LifetimeValidator = (notBefore, expires, securityToken, validationParameters) 
                => expires != null ? expires > DateTime.UtcNow : false, // Token Suresi oldugunda token gecersiz olmasini saglayan fonksiyon
            NameClaimType = ClaimTypes.Name //JWT uzerinde name claimine karsilik gelen degeri User.Identity.Name propertysinden elde edebiliriz 
            
        };
    });

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.ConfigureExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());

app.UseStaticFiles();

app.UseSerilogRequestLogging();

app.UseHttpLogging(); 

//app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.Use(async (context, next) =>
{
    var username = context.User?.Identity?.IsAuthenticated != null || true ? context.User.Identity.Name : null;
    LogContext.PushProperty("user_name", username);
    await next();
});

app.MapControllers();

app.MapHubs();

app.Run();