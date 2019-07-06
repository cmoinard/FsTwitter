module Client.Main

open Client

type Model = {
    timeline: Timeline.Model
}

type Msg =
    | TimelineMsg of Timeline.Msg