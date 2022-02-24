module OsStatsParser.Parsers

open System
open System.Globalization
open System.Text.RegularExpressions

type Entry = { name: string; hours: decimal }
type Stats = { date: DateTime; entries: Entry list }

let entry name hours: Entry = { name = name; hours = hours }

let parseHeading (heading: string): DateTime =
    let _match = Regex.Match(heading, "\d{1,2}/\d{4}")
    if _match.Success
    then DateTime.ParseExact(_match.Value, "M/yyyy", CultureInfo.InvariantCulture)
    else failwith "Heading is missing date in M/YYYY format"

let parseEntry (line: string): Entry =
    let strings = line.Split()

    let hours =
        Decimal.Parse(strings.[2], CultureInfo.InvariantCulture)

    entry strings.[1] hours

let private endOfEntries (line: String) = line.StartsWith("---")

let parseFile (lines: string list): Stats =
    match lines with
    | [] -> failwith "Empty file"
    | heading :: rest ->

        let rec parseEntries (lines: string list) entries =
            match lines with
            | line :: _ when line.StartsWith("---") -> entries
            | line :: rest ->
                let entry = parseEntry line
                parseEntries rest (entry :: entries)
            | [] -> failwith "End of entries marker (---) is missing"

        { date = parseHeading heading
          entries = parseEntries rest [] }
