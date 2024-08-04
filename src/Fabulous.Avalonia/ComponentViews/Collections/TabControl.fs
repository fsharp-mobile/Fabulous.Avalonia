namespace Fabulous.Avalonia.Components

open System.Runtime.CompilerServices
open Avalonia.Controls
open Avalonia.Layout
open Fabulous
open Fabulous.Avalonia
open Fabulous.StackAllocatedCollections

type IFabComponentTabControl =
    inherit IFabComponentSelectingItemsControl
    inherit IFabTabControl

[<AutoOpen>]
module ComponentTabControlBuilders =
    type Fabulous.Avalonia.Components.View with

        /// <summary>Creates a TabControl widget.</summary>
        /// <param name="placement">The placement of the tab strip.</param>
        static member TabControl(placement: Dock) =
            CollectionBuilder<unit, IFabComponentTabControl, IFabComponentTabItem>(TabControl.WidgetKey, ComponentItemsControl.Items, TabControl.TabStripPlacement.WithValue(placement))

        /// <summary>Creates a TabControl widget.</summary>
        static member TabControl() =
            CollectionBuilder<unit, IFabComponentTabControl, IFabComponentTabItem>(TabControl.WidgetKey, ComponentItemsControl.Items, TabControl.TabStripPlacement.WithValue(Dock.Top))