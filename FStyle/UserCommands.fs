module FStyle.Core.UserCommands
open FStyle.Core.Repo
open System

let userCommands(repo: AppRepo) (name: string) = 

    repo.UpdateData (fun data -> 
        let employee = {DisplayName = name}
        Some {data with Employees = (employee :: data.Employees)} )
      