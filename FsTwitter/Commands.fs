namespace FsTwitter

open System
open FsTwitter.Events

type TweetCommand =
    { user: string; content: string; date: DateTime }

type Commands =
    | Tweet of TweetCommand
    
module Command =
    let execute (storeEvent: Event -> unit) command =
        let event =
            match command with
            | Tweet c -> 
                TweetSent {
                    user = c.user
                    content = c.content
                    date = c.date
                }
        
        storeEvent event