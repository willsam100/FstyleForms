module FStyle.Core.Repo

open System.Threading.Tasks

type AppRepo (initialData : AppData, logger: string -> unit) =

    let mutable data = initialData
    let dataLock = new obj()

    let updatedData = Event<unit> ()

    ///// Fired whenever the data changes
    member this.UpdatedData = updatedData.Publish

    member this.Data = data

    member this.UpdateData (update : AppData -> AppData option) = 
        let mutable updated = false
        lock dataLock (fun () ->
            match update data with
            | None -> ()
            | Some newData ->
                data <- newData
                updated <- true )
        if updated then
            logger ("Publishing update: " + List.fold (fun x y-> x + "," + y.DisplayName) "" data.Employees)
            Task.Run updatedData.Trigger |> ignore
            