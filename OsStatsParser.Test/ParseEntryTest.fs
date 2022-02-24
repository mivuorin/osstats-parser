module OsStatsParser.Test.ParseEntryTest

open NUnit.Framework
open OsStatsParser.Parsers
open FsUnit

let valid =
    seq {
        yield TestCaseData("• Test 1.5 h", (entry "Test" 1.5m))
        yield TestCaseData("• First.Last 44 h", (entry "First.Last" 44m))
    }

[<TestCaseSource("valid")>]
let ParseEntry_returns_entry_from_line (line: string) (expected: Entry) =
    parseEntry line
    |> should equal expected
