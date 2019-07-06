module Client.Main.View

open Fable.React
open Fulma

open Client
open Client.Main

let root (model : Model) (dispatch : Msg -> unit) =
    let timeline =
        Timeline.View.root
            model.timeline
            (TimelineMsg >> dispatch)

    div []
        [ Navbar.navbar [ Navbar.Color IsPrimary ]
            [ Navbar.Item.div [ ]
                [ Heading.h2 [ ]
                    [ str "F# Twitter" ] ] ]

          Container.container [] [
              TweetSending.View.root model.tweetSending (TweetSendingMsg >> dispatch)
          ]

          Container.container []
              [ Content.content [ Content.Modifiers [ Modifier.TextAlignment (Screen.All, TextAlignment.Centered) ] ]
                    [ timeline
                    ]
                Columns.columns [] []
              ]
        ]