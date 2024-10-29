namespace Gallery

open System.Diagnostics
open Avalonia.Controls.Primitives
open Avalonia.Layout
open Avalonia.Media
open Fabulous.Avalonia
open Fabulous

open Fabulous.Avalonia.Mvu
open type Fabulous.Avalonia.Mvu.View

module ScrollBarPage =
    type Model = { ScrollValue: float }

    type Msg =
        | ValueChanged of float
        | ScrollBarChanged of ScrollEventArgs

    let init () = { ScrollValue = 0.0 }, Cmd.none

    let update msg model =
        match msg with
        | ValueChanged value -> { ScrollValue = value }, Cmd.none
        | ScrollBarChanged _ -> model, Cmd.none

    let program =
        Program.statefulWithCmd init update
        |> Program.withTrace(fun (format, args) -> Debug.WriteLine(format, box args))
        |> Program.withExceptionHandler(fun ex ->
#if DEBUG
            printfn $"Exception: %s{ex.ToString()}"
            false
#else
            true
#endif
        )

    let view () =
        Component("", program) {
            let! model = Mvu.State

            VStack(spacing = 15.) {

                TextBlock($"Value: {model.ScrollValue}")

                ScrollBar(1., 240., model.ScrollValue, ValueChanged)
                    .orientation(Orientation.Horizontal)
                    .allowAutoHide(false)
                    .background(SolidColorBrush(Colors.LightSalmon))
                    .margin(10., 10., 0., 0.)
                    .onScroll(ScrollBarChanged)
            }
        }
