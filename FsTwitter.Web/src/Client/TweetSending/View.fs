module Client.TweetSending.View

open System
open Fable.React
open Fulma

let button txt onClick =
    Button.button
        [ Button.IsFullWidth
          Button.Color IsPrimary
          Button.OnClick onClick ]
        [ str txt ]

let root (model : Model) (dispatch : Msg -> unit) =
    let sendDisabled =
        model.isSending ||
        String.IsNullOrWhiteSpace model.text

    let errorMessage =
        match model.error with
        | Some e -> str e
        | None -> str ""

    div [] [
        Input.text [
          Input.Disabled model.isSending
          Input.Value model.text
          Input.OnChange (fun e -> dispatch <| EditText e.Value) ]

        Button.button
            [ Button.IsFullWidth
              Button.Color IsPrimary
              Button.IsLoading model.isSending
              Button.Disabled sendDisabled
              Button.OnClick (fun _ -> dispatch Send) ]
            [ str "Tweet"  ]

        errorMessage
    ]
