module Client

open System
open Elmish
open Elmish.React
open Fable.React
open Fable.React.Props
open Fetch.Types
open Thoth.Fetch
open Fulma
open Thoth.Json

open Shared

type Model = { Counter: Counter option }

type TimelineTweet = {
    author: string
    body: string
    date: DateTime
}

type Timeline = {
    tweets: TimelineTweet list
}

type TimelineState =
| Loading
| Error of string
| Loaded of Timeline

type Model' = {
    timeline: TimelineState
}

type Msg =
| Increment
| Decrement
| InitialCountLoaded of Counter

type Msg' =
| LoadTimeline
| ShowTimeline of Timeline
| ShowTimelineError of exn

let initialCounter () = Fetch.fetchAs<Counter> "/api/init"

let getTimeline () =
    Fetch.fetchAs<Timeline> "/api/timeline/chris"

let loadTimeline () : Model' * Cmd<Msg'> =
    let initialModel = { timeline = Loading }
    let initialCmd =
        Cmd.OfPromise.perform
            getTimeline
            ()
            ShowTimeline
    initialModel, initialCmd

let init' () = loadTimeline ()

let update' (msg: Msg') (model: Model') : Model' * Cmd<Msg'> =
    match msg with
    | LoadTimeline ->
        loadTimeline ()

    | ShowTimelineError error ->
        { timeline = Error error.Message }, Cmd.none

    | ShowTimeline t ->
        { timeline = Loaded t }, Cmd.none

let button txt onClick =
    Button.button
        [ Button.IsFullWidth
          Button.Color IsPrimary
          Button.OnClick onClick ]
        [ str txt ]

let showTimeline timeline =
    let htmlTweets =
        timeline.tweets
        |> List.fold (fun acc t -> acc + t.body) ""

    div [ DangerouslySetInnerHTML { __html = htmlTweets } ] []

let showTimelineState =
    function
    | Loading -> str "Loading…"
    | Error err -> str err
    | Loaded t -> showTimeline t

let view (model : Model') (dispatch : Msg' -> unit) =
    div []
        [ Navbar.navbar [ Navbar.Color IsPrimary ]
            [ Navbar.Item.div [ ]
                [ Heading.h2 [ ]
                    [ str "F# Twitter" ] ] ]

          Container.container []
              [ Content.content [ Content.Modifiers [ Modifier.TextAlignment (Screen.All, TextAlignment.Centered) ] ]
                    [ showTimelineState model.timeline
                    ]
                Columns.columns [] []
              ]
        ]

#if DEBUG
open Elmish.Debug
open Elmish.HMR
#endif

Program.mkProgram init' update' view
#if DEBUG
|> Program.withConsoleTrace
#endif
|> Program.withReactBatched "elmish-app"
#if DEBUG
|> Program.withDebugger
#endif
|> Program.run
