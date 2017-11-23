namespace HappyBever

open System.IO
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Logging

module Program =

    [<EntryPoint>]
    let main args =
        let host =
            WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureLogging(fun hostingContext logging ->
                                 logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging")) |> ignore
                                 logging.AddConsole() |> ignore
                                 logging.AddDebug() |> ignore                               
                )
                .ConfigureAppConfiguration(fun hostingContext config ->
                                config.AddJsonFile("appsettings.json", optional = false, reloadOnChange = true) |> ignore
                                config.AddJsonFile((sprintf "appsettings.%s.json" (hostingContext.HostingEnvironment.EnvironmentName)), optional = true) |> ignore
                                config.AddEnvironmentVariables() |> ignore
                )
                .ConfigureServices(fun context services ->

                                services.AddOptions() |> ignore
                )
                .UseApplicationInsights()
                .UseAzureAppServices()
                .UseStartup<Startup>()
                .Build()

        host.Run()

        0
