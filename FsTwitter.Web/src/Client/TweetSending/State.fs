module Client.TweetSending.State

open Elmish
open Thoth.Fetch

let private send (tweet: Model) =
    promise {
        let url = "http://localhost:8080/api/tweet"
        let data =
            { user = "chris"
              text = tweet.text }

        return! Fetch.post(url, data, TweetDto.Decoder)
    }

let init () =
    let model = {
        error =  None
        text = ""
        isSending = false
    }

    model, Cmd.none

let update (msg: Msg) (model: Model) =
    match msg with
    | EditText text ->
        { model with text = text }, Cmd.none

    | TweetSent (Ok _) ->
        init ()

    | TweetSent (Error e) ->
        let model' = { model with error = Some e.Message; isSending = false }
        model', Cmd.none

    | Send ->
        let model' = { model with isSending = true }

        let sendCmd =
            Cmd.OfPromise.either
                send
                model'
                (Ok >> TweetSent)
                (Error >> TweetSent)

        model', sendCmd