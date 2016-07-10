namespace FStyle

open Xamarin.Forms
open FStyle.Repo

type App(repo: AppRepo, cmd: string -> unit, logger: string -> unit) = 
    inherit Application(MainPage = LoginPage(repo, cmd, logger))