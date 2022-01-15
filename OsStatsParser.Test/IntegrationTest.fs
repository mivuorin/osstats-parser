module OsStatsParser.Test.IntegrationTest

open System
open System.Reflection
open NUnit.Framework
open FsUnit
open OsStatsParser

[<SetUp>]
let Setup () = ()


[<Test>]
let Parse_single_file () =
    use stream =
        Assembly
            .GetExecutingAssembly()
            .GetManifestResourceStream("OsStatsParser.Test.report.txt")

    let lines = Data.readFromStream stream

    let actual = Parsers.parseFile lines
    
    let expected: Parsers.Stats =
        { date = DateTime(2019, 1, 1)
          entries =
              [ { name = "Test.Person"; hours = 0.5m }
                { name = "Ruge"; hours = 20m }
                { name = "Random"; hours = 2.5m } ] }

    actual.date |> should equal expected.date
    actual.entries |> should equivalent expected.entries
