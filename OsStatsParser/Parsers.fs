module OsStatsParser.Parsers

open System
open System.Globalization
open System.Text.RegularExpressions

type Entry = { name: string; hours: decimal }
type Stats = { date: DateTime; entries: Entry list }

let entry name hours: Entry = { name = name; hours = hours }

let parseHeading (heading: string): DateTime option =
    let _match = Regex.Match(heading, "\d{1,2}/\d{4}")

    if _match.Success then
        let parsed =
            DateTime.ParseExact(_match.Value, "M/yyyy", CultureInfo.InvariantCulture)

        Some(parsed)
    else
        None

let parseEntry (line: string): Entry =
    let strings = line.Split()

    let hours =
        Decimal.Parse(strings.[2], CultureInfo.InvariantCulture)

    entry strings.[1] hours

let private endOfEntries (line:String) =
    line.StartsWith("---")

let parseFile (lines: string list) =
    let heading = List.head lines
    let rest = List.tail lines
    
    let rec parseEntries (lines: string list) entries =
        match lines with
        | line :: _ when line.StartsWith("---")  ->
            entries
        | line :: rest ->
            let entry = parseEntry line
            parseEntries rest (entry :: entries)
        | [] -> entries // invalid case throw or error

    { date = parseHeading heading |> Option.get // TODO error handling
      entries = parseEntries rest [] }

