module OsStatsParser.Report

open System
open OsStatsParser

type Csv = string list list

type private FlattedEntry =
    { name: string
      date: DateTime
      hours: decimal }

type private PersonGroup =
    { name: string
      hoursForDate: Map<DateTime, decimal> }

let private toMonthString (date: DateTime) = date.ToString("M/yyyy")

let csv (stats: Parsers.Stats list) : Csv =

    let dates =
        stats
        |> List.sortBy (fun s -> s.date)
        |> List.map (fun s -> s.date)

    let header =
        "person" :: (dates |> List.map toMonthString)

    let flattedEntries =
        stats
        |> List.map (fun s ->
            s.entries
            |> List.map (fun e ->
                { name = e.name
                  date = s.date
                  hours = e.hours }))
        |> List.collect id

    let persons =
        flattedEntries
        |> List.groupBy (fun e -> e.name)
        |> List.map (fun g ->
            { name = fst g
              hoursForDate =
                  snd g
                  |> List.map (fun e -> (e.date, e.hours))
                  |> Map })

    let rows =
        persons
        |> List.map (fun person ->
            let name = person.name
            let hours =
                dates
                |> List.map (fun date ->
                    if (person.hoursForDate.ContainsKey date) then
                        person.hoursForDate.[date] |> string
                    else
                        0m |> string)

            name :: hours)

    header :: rows
