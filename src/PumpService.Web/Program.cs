using PumpService.Core;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MariaDB.Extensions;
using PumpService.Services.Channel.Pumps;
using PumpService.Services.Lookups;

Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.File("./LogsFolder/log.txt")//todo get from DB . current directory
                .WriteTo.Console()
                .WriteTo.MariaDB(connectionString: @"server=localhost;port=3306;user=root;password=root;database=TarpetClient",//Data Source=11.11.10.162;Initial Catalog=Sample;User ID=docker;Password=docker
                    tableName: "Log", restrictedToMinimumLevel: LogEventLevel.Information
                )
                .CreateLogger();

Serilog.Debugging.SelfLog.Enable(msg => Console.Error.WriteLine(msg));

try
{
    Log.Information("Starting host");

    var host = CreateHostBuilder(args).Build();

    StartApplication(host);

    host.Run();
}
catch (Exception e)
{
    //.Net 6 add-migration error https://githubmemory.com/repo/dotnet/runtime/issues/60600
    string type = e.GetType().Name;

    if (type.Equals("StopTheHostException", StringComparison.Ordinal))
    {
        throw;
    }

    Log.Fatal(e, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        })
        //.ConfigureWebHost(config =>
        //{
        //    config.UseUrls("http://*:63617");
        //})
        .UseWindowsService()
        .UseSystemd()
        .UseSerilog();

static void StartApplication(IHost host)
{
    using (var scope = host.Services.CreateScope())
    {
        ServiceContainer.Scope = scope;

        var lookupTableService = scope.ServiceProvider.GetService<ILookupTableService>();
        lookupTableService.LoadLookupTable();

        var pumpChannel = scope.ServiceProvider.GetService<IPumpChannel>();
        pumpChannel.StartPumps();
    }
}


