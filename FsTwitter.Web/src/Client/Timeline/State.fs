module Client.Timeline.State

open Elmish
open Thoth.Fetch

open Client.Timeline

let getTimeline () =
    Fetch.fetchAs<Timeline> "/api/timeline/chris"

let loadTimeline () : Model * Cmd<Msg> =
    let initialCmd =
        Cmd.OfPromise.perform
            getTimeline
            ()
            ShowTimeline
    Loading, initialCmd

let init () = loadTimeline ()

let update (msg: Msg) (model: Model) : Model * Cmd<Msg> =
    match msg with
    | LoadTimeline ->
        loadTimeline ()

    | ShowTimelineError error ->
        Error error.Message, Cmd.none

    | ShowTimeline t ->
        Loaded t, Cmd.none