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

        //let mutable viewModel = None

        do base.LoadFromXaml(typeof<LoginPage>) |> ignore
        let employeesList = base.FindByName<ListView>("listView")

        let data =  ([{Name = "Food"; Amount = 100}; {Name = "Takeout"; Amount = 80}; {Name = "Insurance"; Amount = 50}] |> List.toSeq) :> Collections.IEnumerable;
        do employeesList.ItemsSource <- data
       

    
        //let loggerVm vm = 
        //    logger ("udpate list: " + List.fold (fun x y -> x + "," + y.DisplayName) "" vm)
        //    vm
    
        //let UpdateViewModel () =
        //    let newViewModel = Some (createLoginPage repo.Data)
    
        //    let updateList vm = 
        //        employeesList.ItemsSource <- vm |> List.toSeq :> Collections.IEnumerable
    
        //    if viewModel <> newViewModel then
        //        Xamarin.Forms.Device.BeginInvokeOnMainThread (fun x -> 
        //            viewModel <- newViewModel
        //            newViewModel |> Option.map updateList |> ignore )
        //    else 
        //        logger "ViewModel not updated"
        
        
        //let mutable sub = repo.UpdatedData.Subscribe (fun x -> UpdateViewModel())
        //do UpdateViewModel()
    
        //do addEmployee.Clicked.Add(fun x -> 
        //    logger ("Clicked -> name: " + employeeEntry.Text)
        //    cmd employeeEntry.Text)
            