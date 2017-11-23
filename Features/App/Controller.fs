namespace HappyBever.Features.App
open Microsoft.AspNetCore.Mvc
open HappyBever

open Queries

type AppController () =
    inherit Controller()

    // Matcher alle url'er som ikke begynner på /api
    [<Route("{*url:regex(^(?!api).*$)}")>]
    [<Route("")>]
    member this.Index () =
        let getThings = getThings ()

        this.View("~/wwwroot/Index.cshtml", getThings |> Json.serialize)

