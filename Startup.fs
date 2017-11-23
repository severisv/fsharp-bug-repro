namespace HappyBever
open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Configuration
open Microsoft.AspNetCore.Authentication.Cookies
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Logging
open Microsoft.Extensions.Options
open Microsoft.EntityFrameworkCore
open Newtonsoft.Json

type HttpRouteDefaults = { Controller : string; Action : string; ByggId : int }

type Startup () =

    member this.ConfigureServices(services: IServiceCollection) =

        services.AddMvc()
            .AddJsonOptions(fun options -> 
                                options.SerializerSettings.Converters.Add(OptionConverter())
                            ) |> ignore



    member this.Configure(app: IApplicationBuilder, env: IHostingEnvironment) =

        if (env.IsDevelopment()) then
            app.UseDeveloperExceptionPage() |> ignore
        else
            app.UseExceptionHandler("/Error") |> ignore

        app.UseStaticFiles() |> ignore
        app.UseMvc() |> ignore
