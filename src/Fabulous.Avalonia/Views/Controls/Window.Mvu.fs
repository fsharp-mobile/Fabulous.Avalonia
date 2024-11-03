namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls
open Avalonia.Interactivity
open Fabulous
open Fabulous.Avalonia
open Fabulous.StackAllocatedCollections.StackList

module MvuWindow =
    let WindowClosing =
        Attributes.defineEvent "Window_Closing" (fun target -> (target :?> Window).Closing)

    let WindowClosed =
        Attributes.defineRoutedEvent "Window_Closed" Window.WindowClosedEvent

    let WindowOpened =
        Attributes.defineRoutedEvent "Window_Opened" Window.WindowOpenedEvent

[<AutoOpen>]
module MvuWindowBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a Window widget.</summary>
        /// <param name="content">The content of the window.</param>
        static member Window(content: WidgetBuilder<'msg, #IFabElement>) =
            WidgetBuilder<'msg, IFabWindow>(
                Window.WidgetKey,
                AttributesBundle(StackList.empty(), ValueSome [| ContentControl.ContentWidget.WithValue(content.Compile()) |], ValueNone)
            )

        /// <summary>Creates a Window widget.</summary>
        static member Window() =
            SingleChildBuilder<'msg, IFabWindow, 'childMarker>(Window.WidgetKey, ContentControl.ContentWidget)

type MvuWindowModifiers =
    /// <summary>Listens to the Window WindowClosing event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the window is closing.</param>
    [<Extension>]
    static member inline onWindowClosing(this: WidgetBuilder<'msg, #IFabWindow>, fn: WindowClosingEventArgs -> 'msg) =
        this.AddScalar(MvuWindow.WindowClosing.WithValue(fn))

    /// <summary>Listens to the Window WindowClosed event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the window is closed.</param>
    [<Extension>]
    static member inline onWindowClosed(this: WidgetBuilder<'msg, #IFabWindow>, fn: RoutedEventArgs -> 'msg) =
        this.AddScalar(MvuWindow.WindowClosed.WithValue(fn))

    /// <summary>Listens to the Window WindowOpened event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the window is opened.</param>
    [<Extension>]
    static member inline onWindowOpened(this: WidgetBuilder<'msg, #IFabWindow>, fn: RoutedEventArgs -> 'msg) =
        this.AddScalar(MvuWindow.WindowOpened.WithValue(fn))
