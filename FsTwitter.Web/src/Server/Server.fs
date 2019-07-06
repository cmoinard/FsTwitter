open System.IO
open System.Threading.Tasks

open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open FSharp.Control.Tasks.V2
open Giraffe
open Saturn

open FsTwitter.Events
open System
open FsTwitter.Queries
open Microsoft.AspNetCore.Http
open Microsoft.Net.Http.Headers
open FSharp.Control.Tasks.V2.ContextInsensitive
open Giraffe.GiraffeViewEngine

open FsTwitter.Queries
open Shared

let tryGetEnv = System.Environment.GetEnvironmentVariable >> function null | "" -> None | x -> Some x

let publicPath = Path.GetFullPath "../Client/public"

let port =
    "SERVER_PORT"
    |> tryGetEnv |> Option.map uint16 |> Option.defaultValue 8085us


let storage = EventStorage()

let ok () : HttpHandler =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        ctx.SetStatusCode 200
        task { return Some ctx }

let webApp = router {
    getf "/api/timeline/%s" (fun _ ->
        Timeline.query storage.Events
        |> json)

    post "/api/tweet" (fun next ctx ->
        task {
            let! model = ctx.BindJsonAsync<TweetDto>()

            let tweet = { user = model.user; content = model.text; date = DateTime.Now }
            storage.Append <| TweetSent tweet

            return! json model next ctx
        })
}

let app = application {
    url ("http://0.0.0.0:" + port.ToString() + "/")
    use_router webApp
    memory_cache
    use_static publicPath
    use_json_serializer(Thoth.Json.Giraffe.ThothSerializer())
    use_gzip
}

run app
