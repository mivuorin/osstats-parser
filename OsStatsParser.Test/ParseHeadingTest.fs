module OsStatsParser.Test.ParseHeadingTest

open System
open NUnit.Framework
open OsStatsParser

[<Test>]
let ParseHeading_returns_date_from_heading () =
    let heading =
        "Reported OS-contributions in #opensource for 1/2019 in all office(s)"

    let actual = Parsers.parseHeading heading
    let expected = Some(DateTime(2019, 1, 1))
    Assert.AreEqual(expected, actual)

[<Test>]
let ParseHeading_double_digit_month () =
    let heading =
        "Reported OS-contributions in #opensource for 12/2022 in all office(s)"

    let actual = Parsers.parseHeading heading
    let expected = Some(DateTime(2022, 12, 1))
    Assert.AreEqual(expected, actual)

[<Test>]
let ParseHeading_returns_none_when_heading_does_not_contain_date () =
    let actual = Parsers.parseHeading "Heading without date"
    Assert.AreEqual(None, actual)
