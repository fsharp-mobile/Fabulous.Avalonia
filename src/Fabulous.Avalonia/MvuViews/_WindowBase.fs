namespace Fabulous.Avalonia.Mvu

open System.Runtime.CompilerServices
open Avalonia.Controls
open Fabulous
open Fabulous.Avalonia

type IFabMvuWindowBase =
    inherit IFabMvuTopLevel
    inherit IFabWindowBase

module MvuWindowBase =
    let Activated =
        MvuAttributes.defineEventNoArg "WindowBase_Activated" (fun target -> (target :?> WindowBase).Activated)

    let Deactivated =
        MvuAttributes.defineEventNoArg "WindowBase_Deactivated" (fun target -> (target :?> WindowBase).Deactivated)

    let PositionChanged =
        MvuAttributes.defineEvent "WindowBase_PositionChanged" (fun target -> (target :?> WindowBase).PositionChanged)

    let Resized =
        MvuAttributes.defineEvent "WindowBase_Resized" (fun target -> (target :?> WindowBase).Resized)

type MvuWindowBaseModifiers =
    /// <summary>Listens to the WindowBase Activated event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="msg">Raised when the window is activated.</param>
    [<Extension>]
    static member inline onActivated(this: WidgetBuilder<unit, #IFabMvuWindowBase>, msg: unit -> unit) =
        this.AddScalar(MvuWindowBase.Activated.WithValue(msg))

    /// <summary>Listens to the WindowBase Deactivated event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="msg">Raised when the window is deactivated.</param>
    [<Extension>]
    static member inline onDeactivated(this: WidgetBuilder<'msg, #IFabMvuWindowBase>, msg: unit -> unit) =
        this.AddScalar(MvuWindowBase.Deactivated.WithValue(msg))

    /// <summary>Listens to the WindowBase PositionChanged event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the window position is changed.</param>
    [<Extension>]
    static member inline onPositionChanged(this: WidgetBuilder<unit, #IFabMvuWindowBase>, fn: PixelPointEventArgs -> unit) =
        this.AddScalar(MvuWindowBase.PositionChanged.WithValue(fn))

    /// <summary>Listens to the WindowBase Resized event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the window is resized.</param>
    [<Extension>]
    static member inline onResized(this: WidgetBuilder<unit, #IFabMvuWindowBase>, fn: WindowResizedEventArgs -> unit) =
        this.AddScalar(MvuWindowBase.Resized.WithValue(fn))
