module FsTwitter.Tests.Timeline

open FsTwitter
open FsTwitter.Events
open FsTwitter.Queries
open System
open Xunit
open Swensen.Unquote 

[<Fact>]
let ``Should show its own tweets when no followers`` () =

    let date = DateTime(2019, 07, 01)
    let storage = EventStorage()
    
    let tweet =
        Tweet {
          user = "chris"
          content = "toto"
          date = date }
    
    tweet |> Command.execute storage.Append
    
    
    let actual = Timeline.query storage.Events
    
    let expected = {
        tweets = [
            { author = "chris"
              body = "<div>toto</div>"
              date = date }
        ]
    }
    
    actual =! expected
