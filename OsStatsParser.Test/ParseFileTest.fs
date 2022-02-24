module OsStatsParser.Test.ParseFileTest

open System
open NUnit.Framework
open FsUnit
open OsStatsParser

let heading =
    "Reported OS-contributions in #opensource for 1/2019 in all office(s)"

[<Test>]
let ParseFile_should_throw_when_empty_lines () =
    (fun () -> Parsers.parseFile [] |> ignore)
    |> should (throwWithMessage "Empty file") typeof<Exception>

[<Test>]
let ParseFile_should_throw_when_no_end_marker () =
    let file = [ heading ]

    (fun () -> Parsers.parseFile file |> ignore)
    |> should (throwWithMessage "End of entries marker (---) is missing") typeof<Exception>
