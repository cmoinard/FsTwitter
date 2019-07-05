namespace FsTwitter.Queries

open System
open FsTwitter.Events

type TimelineTweet = {
    author: string
    body: string
    date: DateTime
}

type Timeline = {
    tweets: TimelineTweet list
}

module Timeline =
    let query events =
        let sentTweets =
            events
            |> List.choose (fun e ->
                match e with
                | TweetSent t -> Some t)
        
        let toTimelineTweet t = {
            author = t.user
            body = sprintf "<div>%s</div>" t.content
            date = t.date
        }
        
        { tweets = sentTweets |> List.map toTimelineTweet }