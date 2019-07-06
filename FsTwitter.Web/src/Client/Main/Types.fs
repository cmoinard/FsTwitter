namespace Client.Main

open Client

type Model = {
    timeline: Timeline.Model
    tweetSending: TweetSending.Model
}

type Msg =
    | TimelineMsg of Timeline.Msg
    | TweetSendingMsg of TweetSending.Msg