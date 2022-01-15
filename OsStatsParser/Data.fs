module OsStatsParser.Data

open System.IO

let readFromStream (stream: Stream) =
    seq {
        use reader = new StreamReader(stream, true)
        
        while not reader.EndOfStream do
            yield reader.ReadLine()
    } |> Seq.toList // TODO Use reader readAllLines
    
let readFromPath (path:string) =
    use stream = File.OpenRead(path)
    readFromStream (stream)