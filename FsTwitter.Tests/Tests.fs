module Tests

open Xunit
open Swensen.Unquote

[<Fact>]
let ``My test`` () =
    test <@ 1 + 1 = 2 @>
