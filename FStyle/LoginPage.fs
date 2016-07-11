namespace FStyle

open System
open Xamarin.Forms
open Xamarin.Forms.Xaml
open FStyle.Repo
open FStyle.ViewModels

// UI Class 
type LoginPage(repo: AppRepo, cmd: string -> unit, logger: string -> unit) =
    inherit ContentPage() 
    
    //do base.LoadFromXaml(typeof<LoginPage>) |> ignore
    
    
//   <ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
//             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
//             x:Class="FStyle.LoginPage">
//        <ContentPage.Padding>
//            <OnPlatform x:TypeArguments="Thickness" iOS="0,20,0,0" Android="10,0,0,10"/>
//        </ContentPage.Padding>
//        <StackLayout VerticalOptions="FillAndExpand" HorizontalOptions="FillAndExpand">
//            <StackLayout Orientation = "Horizontal" HorizontalOptions="FillAndExpand">
//                 <Entry x:Name="employeeEntry" Placeholder="Employee name" HorizontalOptions="FillAndExpand"/>                  
//                 <Button x:Name="addEmployeeButton" Text="Add Employee" WidthRequest="100"/>
//            </StackLayout>
//            <ListView x:Name="EmployeeView">
//                <ListView.ItemTemplate>
//                    <DataTemplate>
//                      <ViewCell>
//                        <ViewCell.View>
//                          <StackLayout>
//                            <Label Text="{Binding DisplayName}"  />
//                          </StackLayout>
//                        </ViewCell.View>
//                      </ViewCell>
//                    </DataTemplate>
//                </ListView.ItemTemplate>
//            </ListView>
//    </StackLayout>
//</ContentPage>


    let employeeEntry = Entry(HorizontalOptions=LayoutOptions.FillAndExpand)
    employeeEntry.Placeholder <- "Employee Name"
    let employeeButton = Button(Text="Add Employee")
    employeeButton.WidthRequest <- "100"
    
    
    
    
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
        