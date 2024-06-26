using Serilog;
using Pathmaker.Api.Configuration.ApiVersioning;
using Pathmaker.Api.Configuration.HealthChecks;
using Pathmaker.Api.Configuration.JsonSerilizer;
using Pathmaker.Api.Configuration.Logging;
using Pathmaker.Api.Configuration.ServicesValidation;
using Pathmaker.Api.Configuration.Swagger;
using Pathmaker.Application.Extensions;
using Pathmaker.Infrastructure.Extensions;
using Pathmaker.Shared.Extensions;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

Log.Information("Starting up");

try {
    RunApplication();
}


catch (Exception ex) {
    Log.Fatal(ex, "Unhandled exception");
}
finally {
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}


void RunApplication() {
    var builder = WebApplication.CreateBuilder(args);
    // Logging
    builder.Host.UseSerilog((ctx, lc) => lc
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .ReadFrom.Configuration(ctx.Configuration));
    // Add services to the container.
    builder.Services.AddShared(builder.Configuration);
    builder.Services.AddApplication(builder.Configuration, builder.Environment);
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddHealthChecks(builder.Configuration, builder.Environment);
    builder.Services.AddControllers().AddJsonSerializer();
    builder.Services.AddDefaultApiVersioning();
    builder.Services.AddCors();
    builder.Services.AddSwagger();
    builder.Host.AddServicesValidationOnStart();

    var app = builder.Build();
    app.UseCors(policyBuilder => { policyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
    app.UseSerilogRequestLogging(options => { options.GetLevel = LogHelper.ExcludeHealthChecks; });
    app.UseApplication();
    // Configure the HTTP request pipeline.
    if (!app.Environment.IsProduction()) {
        app.UseSwaggerUi();
    }

    app.MapHealthChecks();
    app.MapControllers();
    
    app.AddMigration();

    app.Run();
}
