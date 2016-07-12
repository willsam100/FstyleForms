namespace FStyle.Droid
open System;

open Android.App;
open Android.Content;
open Android.Content.PM;
open Android.Runtime;
open Android.Views;
open Android.Widget;
open Android.OS;
open FStyle
open FStyle.Repo
open FStyle.UserCommands

[<Activity (Label = "FStyle.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = (ConfigChanges.ScreenSize ||| ConfigChanges.Orientation))>]
type MainActivity() =
    inherit Xamarin.Forms.Platform.Android.FormsApplicationActivity()
    override this.OnCreate (bundle: Bundle) =
        base.OnCreate (bundle)

        let appData = {Employees = [{DisplayName = "Sam"}; {DisplayName = "Sol"}]} 
        let repo = AppRepo(appData, Console.WriteLine)
        let cmds = userCommands repo

        Xamarin.Forms.Forms.Init (this, bundle)
        this.LoadApplication (new App (repo, cmds, Console.WriteLine))