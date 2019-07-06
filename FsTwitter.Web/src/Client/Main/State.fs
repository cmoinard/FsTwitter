module Client.Main.State

open Elmish

open Client
open Client.Main

let init () =
    let (timelineModel, timelineMsg) = Timeline.State.init ()
    let (editModel, editMsg) = TweetSending.State.init ()

    let model =
        { timeline = timelineModel
          tweetSending = editModel }

    let cmd = Cmd.batch [
        Cmd.map TimelineMsg timelineMsg
        Cmd.map TweetSendingMsg editMsg
    ]

    model, cmd

let update (msg: Msg) (model: Model) =
    match msg with
    | TimelineMsg m ->
        let (model', msg') = Timeline.State.update m model.timeline

        { model with timeline = model' }, Cmd.map TimelineMsg msg'

    | TweetSendingMsg m ->
        let (model', msg') = TweetSending.State.update m model.tweetSending

        let additionalCmds =
            match m with
            | TweetSending.TweetSent (Ok _) ->
                [ TimelineMsg Timeline.LoadTimeline |> Cmd.ofMsg ]
            | _ ->
                []

        let newModel = { model with tweetSending = model' }
        let newCmd =
            seq {
                yield Cmd.map TweetSendingMsg msg'
                yield! additionalCmds
            }


        newModel, Cmd.batch newCmd