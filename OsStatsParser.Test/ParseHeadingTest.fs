module OsStatsParser.Test.ParseHeadingTest

open System
open NUnit.Framework
open OsStatsParser
open FsUnit

[<Test>]
let ParseHeading_returns_date_from_heading () =
    "Reported OS-contributions in #opensource for 1/2019 in all office(s)"
    |> Parsers.parseHeading
    |> should equal
    <| DateTime(2019, 1, 1)


[<Test>]
let ParseHeading_double_digit_month () =
    "Reported OS-contributions in #opensource for 12/2022 in all office(s)"
    |> Parsers.parseHeading
    |> should equal
    <| DateTime(2022, 12, 1)

[<Test>]
let ParseHeading_throws_when_heading_does_not_contain_date () =
    (fun () ->
        Parsers.parseHeading "Heading without date"
        |> ignore)
    |> should (throwWithMessage "Heading is missing date in M/YYYY format") typeof<Exception>
