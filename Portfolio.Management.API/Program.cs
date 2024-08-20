using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Portfolio.Management.API.Middleware;
using Portfolio.Management.Domain.Configuration;
using Portfolio.Management.Domain.Repositories;
using Portfolio.Management.Domain.Services.FinancialProduct;
using Portfolio.Management.Domain.Services.Investiment;
using Portfolio.Management.Domain.Services.Notification;
using Portfolio.Management.Domain.Services.Portoflio;
using Portfolio.Management.Infra.Data.Context;
using Portfolio.Management.Infra.Data.Repositories.Dapper;
using Portfolio.Management.Infra.Data.Repositories.EntityFramework;
using System.Net.Mail;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services, builder.Configuration, builder);

var app = builder.Build();
ConfigureApp(app, builder.Environment);

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.Run();

static void ConfigureServices(IServiceCollection services, IConfiguration configuration, WebApplicationBuilder builder)
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();

    services.AddSingleton<IEmailService, EmailService>();
    services.AddSingleton<INotificationService, NotificationService>();
    services.AddScoped<IFinancialProductService, FinancialProductService>();
    services.AddScoped<IPortfolioService, PortfolioService>();

    services.AddSingleton<IDataReadRepository>(sp => new DataReaderRepository(configuration.GetConnectionString("PORTFOLIO")));

    services.AddDbContext<DataContext>(options => options.UseSqlServer(configuration.GetConnectionString("PORTFOLIO")));
    services.AddScoped<IDataWritterRepository, DataWritterRepository>();

    services.AddHostedService<DailyNotificationHostedService>();

    services.AddTransient<SmtpClient>();

    var emailConfig = configuration.GetSection(nameof(EmailConfiguration)).Get<EmailConfiguration>();
    services.AddSingleton(emailConfig);

    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Portfolio.Management.Api", Version = "v1" });

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
    });
}

static void ConfigureApp(WebApplication app, IWebHostEnvironment environment)
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
    });

    if (environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
}
