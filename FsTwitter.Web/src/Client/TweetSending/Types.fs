namespace Client.TweetSending

open Thoth.Json

type Model = {
    text: string
    isSending: bool
    error: string option
}

type TweetDto =
    { user: string
      text: string
    }
    with
    static member Decoder =
        Decode.object (fun get ->
            { user = get.Required.Field "user" Decode.string
              text = get.Required.Field "text" Decode.string
            })

type Msg =
    | EditText of string
    | Send
    | TweetSent of Result<TweetDto, exn>