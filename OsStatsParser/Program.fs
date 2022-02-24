open OsStatsParser
open System
open System.IO

open Argu

type CliArguments =
    | Data of path: string
    | Output of path: string

    interface IArgParserTemplate with
        member s.Usage =
            match s with
            | Data _ -> "Input data folder. eg. ./data"
            | Output _ -> "Output file name. eg. output.csv"


[<EntryPoint>]
let main argv =

    let parser =
        ArgumentParser.Create<CliArguments>(errorHandler = ProcessExiter())

    let results = parser.Parse argv

    let folder =
        results.GetResult(Data, defaultValue = "./data")

    let output =
        results.GetResult(Output, defaultValue = "./output.csv")

    let stats =
        Directory.GetFiles folder
        |> Array.toList
        |> List.map Data.readFromPath
        |> List.map Parsers.parseFile

    let csv = Report.csv stats

    use writer = new StreamWriter(output)

    for values in csv do
        let row = String.Join(',', values)
        writer.WriteLine(row)

    0
