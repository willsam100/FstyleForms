namespace FStyle

open Xamarin.Forms
open FStyle.Repo
open FStyle

type App(repo: AppRepo, cmd: string -> unit, logger: string -> unit) = 
    inherit Application(MainPage = LoginPage(repo, cmd, logger))