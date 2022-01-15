open OsStatsParser
open System
open System.IO
open System.Text.Json


[<EntryPoint>]
let main argv =

    // TODO Proper argument parsing
    let folder = argv.[0]
    let output = argv.[1]

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
    
    //     JsonSerializer.Serialize(stats) |> printf "%s"

    0
