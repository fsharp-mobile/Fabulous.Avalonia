﻿namespace Fabulous.Avalonia.Mvu

open Avalonia.Interactivity
open Fabulous
open Fabulous.Avalonia
open Fabulous.StackAllocatedCollections.StackList

type IFabMvuDropDownButton =
    inherit IFabMvuButton
    inherit IFabDropDownButton

[<AutoOpen>]
module MvuDropDownButtonBuilders =
    type Fabulous.Avalonia.Mvu.View with

        /// <summary>Creates a DropDownButton widget.</summary>
        /// <param name="text">The text to display.</param>
        /// <param name="fn">Raised when the DropDownButton is clicked.</param>
        static member DropDownButton(text: string, fn: RoutedEventArgs -> 'msg) =
            WidgetBuilder<'msg, IFabMvuDropDownButton>(DropDownButton.WidgetKey, ContentControl.ContentString.WithValue(text), MvuButton.Clicked.WithValue(fn))

        /// <summary>Creates a DropDownButton widget.</summary>
        /// <param name="fn">Raised when the DropDownButton is clicked.</param>
        /// <param name="content">The content of the DropDownButton.</param>
        static member DropDownButton(fn: RoutedEventArgs -> 'msg, content: WidgetBuilder<'msg, #IFabMvuControl>) =
            WidgetBuilder<'msg, IFabMvuDropDownButton>(
                DropDownButton.WidgetKey,
                AttributesBundle(
                    StackList.one(MvuButton.Clicked.WithValue(fn)),
                    ValueSome [| ContentControl.ContentWidget.WithValue(content.Compile()) |],
                    ValueNone
                )
            )
