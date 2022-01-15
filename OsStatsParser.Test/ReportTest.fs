module OsStatsParser.Test.ReportTest

open System
open OsStatsParser
open NUnit.Framework
open FsUnit

[<Test>]
let Csv_export () =
    let stats: Parsers.Stats list =
        [ { date = DateTime(2019, 1, 1)
            entries = [ Parsers.entry "ruge" 1m ] }
          { date = DateTime(2022, 1, 1)
            entries = [ Parsers.entry "random" 2m ] }
          { date = DateTime(2022, 2, 1)
            entries =
                [ Parsers.entry "ruge" 3m
                  Parsers.entry "test" 1.5m ] } ]

    let actual = Report.csv stats

    let expectedHeaders =
        [ "person"
          "1/2019"
          "1/2022"
          "2/2022" ]

    List.head actual |> should equal expectedHeaders
    
    let expectedRows =
        [["ruge"; "1"; "0"; "3"]
         ["random"; "0"; "2"; "0"]
         ["test"; "0"; "0"; "1.5"]]
        
    List.tail actual |> should equal expectedRows
