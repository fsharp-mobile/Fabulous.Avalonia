namespace Fabulous.Avalonia

open System
open System.Runtime.CompilerServices
open Avalonia.Controls
open Fabulous
open Fabulous.Avalonia

module MvuNativeMenu =
    let Opening =
        Attributes.Mvu.defineEvent "NativeMenu_Opening" (fun target -> (target :?> NativeMenu).Opening)

    let Closed =
        Attributes.Mvu.defineEvent "NativeMenu_Opening" (fun target -> (target :?> NativeMenu).Closed)

    let NeedsUpdate =
        Attributes.Mvu.defineEvent "NativeMenu_NeedsUpdate" (fun target -> (target :?> NativeMenu).NeedsUpdate)

type MvuNativeMenuModifiers =
    /// <summary>Listens to the NativeMenu Opening event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="msg">Raised when the Opening event fires.</param>
    [<Extension>]
    static member inline onOpening(this: WidgetBuilder<'msg, #IFabNativeMenu>, msg: EventArgs -> 'msg) =
        this.AddScalar(MvuNativeMenu.Opening.WithValue(msg))

    /// <summary>Listens to the NativeMenu Closed event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="msg">Raised when the Closed event fires.</param>
    [<Extension>]
    static member inline onClosed(this: WidgetBuilder<'msg, #IFabNativeMenu>, msg: EventArgs -> 'msg) =
        this.AddScalar(MvuNativeMenu.Closed.WithValue(msg))

    /// <summary>Listens to the NativeMenu NeedsUpdate event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="msg">Raised when the NeedsUpdate event fires.</param>
    [<Extension>]
    static member inline onNeedsUpdate(this: WidgetBuilder<'msg, #IFabNativeMenu>, msg: EventArgs -> 'msg) =
        this.AddScalar(MvuNativeMenu.NeedsUpdate.WithValue(msg))
