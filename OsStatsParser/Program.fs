open OsStatsParser
open System
open System.IO
open System.Text.Json


[<EntryPoint>]
let main argv =

    let folder = argv.[0]

    let stats =
        Directory.GetFiles folder
        |> Array.map Data.readFromPath
        |> Array.map Parsers.parseFile

    JsonSerializer.Serialize(stats) |> printf "%s"
    0
