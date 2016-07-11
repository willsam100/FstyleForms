module FStyle.ViewModels
open System
open FStyle.Repo

type LoginPageViewModel = {
    Data : Employee list 
    Actions: (string -> unit) list
}

let createLoginPage (data: AppData) =

    //let cmd name = repo.UpdateData (fun data -> 
    //    let employee = {DisplayName = name}
    //    Some {data with Employees = (employee :: data.Employees)} )

   data.Employees