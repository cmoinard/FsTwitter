module Client.Main.State

open Elmish

open Client
open Client.Main

let init () =
    let (timelineModel, timelineMsg) = Timeline.State.init ()

    let model = { timeline = timelineModel }
    let cmd = Cmd.batch [
        Cmd.map TimelineMsg timelineMsg
    ]

    model, cmd

let update (msg: Msg) (model: Model) =
    match msg with
    | TimelineMsg m ->
        let (timelineModel, timelineMsg) = Timeline.State.update m model.timeline
        
        { model with timeline = timelineModel }, Cmd.map TimelineMsg timelineMsg