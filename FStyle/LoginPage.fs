namespace FStyle

open System
open Xamarin.Forms
open Xamarin.Forms.Xaml
open FStyle.Repo
open FStyle.ViewModels


type CategoryCell() = 
    inherit ViewCell()

    let init = 
        let horizontalLayout = new StackLayout (Orientation=StackOrientation.Horizontal, BackgroundColor = Color.FromHex ("#eee"));
        let left = new Label (TextColor = Color.FromHex ("#f35e20"));
        let right = new Label (HorizontalOptions = LayoutOptions.End, TextColor = Color.FromHex ("503026"));

        left.SetBinding (Label.TextProperty, "Category");
        right.SetBinding (Label.TextProperty, "Amount");

            ////add views to the view hierarchy
        horizontalLayout.Children.Add (left);
        horizontalLayout.Children.Add (right);
        base.View <- horizontalLayout;
    do init 




// UI Class 
type LoginPage(repo: AppRepo, cmd: string -> unit, logger: string -> unit) =
    inherit ContentPage() 
    
    //do base.LoadFromXaml(typeof<LoginPage>) |> ignore


    let createGui = 

        let employeeEntry = Entry(HorizontalOptions=LayoutOptions.FillAndExpand)
        employeeEntry.Placeholder <- "Employee Name"
        let employeeButton = Button(Text="Add Employee")
        employeeButton.WidthRequest <- 100.
        let dataTemplate = DataTemplate(typeof<CategoryCell>)

        let categories = ListView(ItemTemplate = dataTemplate)
        Content <- categories
    
    
    
    
    let mutable viewModel = None
    //let employeesList = base.FindByName<ListView>("EmployeeView")
    //let employeeEntry = base.FindByName<Entry>("employeeEntry")
    //let addEmployee = base.FindByName<Button>("addEmployeeButton")


    let loggerVm vm = 
        logger ("udpate list: " + List.fold (fun x y -> x + "," + y.DisplayName) "" vm)
        vm

    let UpdateViewModel () =
        let newViewModel = Some (createLoginPage repo.Data)

        let updateList vm = 
            employeesList.ItemsSource <- vm |> List.toSeq :> Collections.IEnumerable

        if viewModel <> newViewModel then
            Xamarin.Forms.Device.BeginInvokeOnMainThread (fun x -> 
                viewModel <- newViewModel
                newViewModel |> Option.map updateList |> ignore )
        else 
            logger "ViewModel not updated"
    
    
    let mutable sub = repo.UpdatedData.Subscribe (fun x -> UpdateViewModel())
    do UpdateViewModel()

    do addEmployee.Clicked.Add(fun x -> 
        logger ("Clicked -> name: " + employeeEntry.Text)
        cmd employeeEntry.Text)
        