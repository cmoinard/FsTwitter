module Client.Timeline.View

open Fable.React
open Fable.React.Props
open Fulma

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

let root (model : Model) (dispatch : Msg -> unit) =
    match model with
    | Loading -> str "Loading…"
    | Error err -> str err
    | Loaded t -> showTimeline t