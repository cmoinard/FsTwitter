namespace Client.Timeline

open System

type TimelineTweet = {
    author: string
    body: string
    date: DateTime
}

type Timeline = {
    tweets: TimelineTweet list
}

type Model =
| Loading
| Error of string
| Loaded of Timeline


type Msg =
| LoadTimeline
| ShowTimeline of Timeline
| ShowTimelineError of exn

