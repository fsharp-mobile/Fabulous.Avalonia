namespace HelloComponent.Android

open Android.App
open Android.Content.PM
open Avalonia
open Avalonia.Android
open Fabulous.Avalonia
open HelloComponent

[<Activity(Label = "NewApp.Android",
           Theme = "@style/MyTheme.NoActionBar",
           Icon = "@drawable/icon",
           LaunchMode = LaunchMode.SingleTop,
           ConfigurationChanges = (ConfigChanges.Orientation ||| ConfigChanges.ScreenSize))>]
type MainActivity() =
    inherit AvaloniaMainActivity<FabApplication>()

    override this.CustomizeAppBuilder(_builder: AppBuilder) =
        AppBuilder.UseFabulousApp(App.view, App.theme)
