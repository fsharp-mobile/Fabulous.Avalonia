namespace Fabulous.Avalonia.Components

open System.Runtime.CompilerServices
open Avalonia.Controls
open Fabulous
open Fabulous.Avalonia

type IFabComponentGrid =
    inherit IFabComponentPanel
    inherit IFabGrid

[<AutoOpen>]
module ComponentGridBuilders =
    type Fabulous.Avalonia.Components.View with

        /// <summary>Creates a Grid widget.</summary>
        /// <param name="coldefs">Column definitions.</param>
        /// <param name="rowdefs">Row definitions.</param>
        static member Grid(coldefs: seq<Dimension>, rowdefs: seq<Dimension>) =
            CollectionBuilder<unit, IFabComponentGrid, IFabComponentControl>(
                Grid.WidgetKey,
                ComponentPanel.Children,
                Grid.ColumnDefinitions.WithValue(Array.ofSeq coldefs),
                Grid.RowDefinitions.WithValue(Array.ofSeq rowdefs)
            )

        /// <summary>Creates a Grid widget with a single column and row.</summary>
        static member Grid() = View.Grid([ Star ], [ Star ])