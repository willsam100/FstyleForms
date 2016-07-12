namespace FStyle.Core

open Xamarin.Forms
open FStyle.Core.Repo

type App(repo: AppRepo, cmd: string -> unit, logger: string -> unit) = 
    inherit Application(MainPage = LoginPage(repo, cmd, logger))