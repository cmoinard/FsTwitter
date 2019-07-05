namespace FsTwitter.Events

open System

type TweetSentEvent =
    { user: string; content: string; date: DateTime }

type Event =
    | TweetSent of TweetSentEvent
    
    
    
type EventStorage() =
    let mutable events: Event list = []
    
    member __.Append(event) =
        events <- events |> List.append [event]
        
    member __.Events = events