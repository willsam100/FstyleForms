namespace FStyle

open System
open Xamarin.Forms
open Xamarin.Forms.Xaml
open FStyle.Repo
open FStyle.ViewModels

// UI Class 
type LoginPage(repo: AppRepo, logger: string -> unit) =
    inherit ContentPage() 
    
    do base.LoadFromXaml(typeof<LoginPage>) |> ignore
    let mutable viewModel = None
    let employeesList = base.FindByName<ListView>("EmployeeView")
    let employeeEntry = base.FindByName<Entry>("employeeEntry")
    let addEmployee = base.FindByName<Button>("addEmployeeButton")


    let loggerVm vm = 
        logger ("udpate list: " + List.fold (fun x y -> x + "," + y.DisplayName) "" vm)
        vm



    let UpdateViewModel () =
        let newViewModel = Some (createLoginPage repo)

        let updateList (vm: LoginPageViewModel) = 
            employeesList.ItemsSource <- vm.Data |> List.toSeq :> Collections.IEnumerable

        if viewModel <> newViewModel then
            Xamarin.Forms.Device.BeginInvokeOnMainThread (fun x -> 
            viewModel <- newViewModel
            newViewModel |> Option.map (updateList) |> ignore )
        else 
            logger "ViewModel not updated"
    
    
    let mutable sub = repo.UpdatedData.Subscribe (fun x -> UpdateViewModel())
    do UpdateViewModel()

    do addEmployee.Clicked.Add(fun x -> 
        logger ("Clicked -> name: " + employeeEntry.Text)
        cmd employeeEntry.Text)
        