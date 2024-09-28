namespace Fabulous.Avalonia.Mvu

open System.Runtime.CompilerServices
open Avalonia.Controls
open Avalonia.Interactivity
open Fabulous
open Fabulous.Avalonia
open Fabulous.StackAllocatedCollections.StackList

type IFabMvuSplitButton =
    inherit IFabMvuContentControl
    inherit IFabSplitButton

module MvuSplitButton =
    let Clicked =
        MvuAttributes.defineEvent "SplitButton_Clicked" (fun target -> (target :?> SplitButton).Click)

[<AutoOpen>]
module MvuSplitButtonBuilders =
    type Fabulous.Avalonia.Mvu.View with

        /// <summary>Creates a SplitButton widget.</summary>
        /// <param name="text">The text to display.</param>
        /// <param name="fn">Raised when the SplitButton is clicked.</param>
        static member SplitButton(text: string, fn: RoutedEventArgs -> unit) =
            WidgetBuilder<'msg, IFabSplitButton>(
                SplitButton.WidgetKey,
                ContentControl.ContentString.WithValue(text),
                MvuSplitButton.Clicked.WithValue(fn)
            )

        /// <summary>Creates a SplitButton widget.</summary>
        /// <param name="fn">Raised when the SplitButton is clicked.</param>
        /// <param name="content">The content to display in the flyout.</param>
        static member SplitButton(fn: RoutedEventArgs -> unit, content: WidgetBuilder<unit, #IFabControl>) =
            WidgetBuilder<'msg, IFabSplitButton>(
                SplitButton.WidgetKey,
                AttributesBundle(
                    StackList.one(MvuSplitButton.Clicked.WithValue(fn)),
                    ValueSome [| ContentControl.ContentWidget.WithValue(content.Compile()) |],
                    ValueNone
                )
            )

type MvuSplitButtonModifiers =
    /// <summary>Link a ViewRef to access the direct SplitButton control instance.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control.</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabSplitButton>, value: ViewRef<SplitButton>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
