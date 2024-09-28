namespace Fabulous.Avalonia.Components

open System.Runtime.CompilerServices
open Avalonia.Controls
open Avalonia.Layout
open Avalonia.Media
open Fabulous
open Fabulous.Avalonia
open Fabulous.StackAllocatedCollections
open Fabulous.StackAllocatedCollections.StackList

type IFabComponentComboBox =
    inherit IFabComponentSelectingItemsControl
    inherit IFabComboBox

module ComponentComboBox =
    let DropDownOpened =
        ComponentAttributes.defineAvaloniaPropertyWithChangedEvent' "Opened" ComboBox.IsDropDownOpenProperty


[<AutoOpen>]
module ComboBoxBuilders =
    type Fabulous.Avalonia.Components.View with

        /// <summary>Creates a ComboBox widget.</summary>
        /// <param name="items">The items to display in the ComboBox.</param>
        static member ComboBox(items: seq<_>) =
            WidgetBuilder<unit, IFabComponentComboBox>(
                ComboBox.WidgetKey,
                AttributesBundle(StackList.one(ItemsControl.ItemsSource.WithValue(items)), ValueNone, ValueNone)
            )

        /// <summary>Creates a ComboBox widget.</summary>
        /// <param name="items">The items to display in the ComboBox.</param>
        /// <param name="template">The template to use to render each item.</param>
        static member ComboBox(items: seq<'itemData>, template: 'itemData -> WidgetBuilder<unit, 'itemMarker>) =
            WidgetHelpers.buildItems<unit, IFabComponentComboBox, 'itemData, 'itemMarker> ComboBox.WidgetKey ItemsControl.ItemsSourceTemplate items template

        /// <summary>Creates a ComboBox widget.</summary>
        static member ComboBox() =
            CollectionBuilder<unit, IFabComponentComboBox, IFabComponentComboBoxItem>(ComboBox.WidgetKey, ComponentItemsControl.Items)

type ComponentComboBoxModifiers =
    /// <summary>Listens to the ComboBox DropDownOpened event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="isOpen">Weather the drop down is open or not.</param>
    /// <param name="fn">Raised when the DropDownOpened event fires.</param>
    [<Extension>]
    static member inline onDropDownOpened(this: WidgetBuilder<'msg, #IFabComponentComboBox>, isOpen: bool, fn: bool -> unit) =
        this.AddScalar(ComponentComboBox.DropDownOpened.WithValue(ComponentValueEventData.create isOpen fn))

    /// <summary>Link a ViewRef to access the direct ComboBox control instance.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control.</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabComponentComboBox>, value: ViewRef<ComboBox>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))

type ComponentComboBoxCollectionBuilderExtensions =
    [<Extension>]
    static member inline Yield<'msg, 'marker, 'itemType when 'itemType :> IFabComboBoxItem>
        (_: CollectionBuilder<'msg, 'marker, IFabComboBoxItem>, x: WidgetBuilder<'msg, 'itemType>)
        : Content<'msg> =
        { Widgets = MutStackArray1.One(x.Compile()) }

    [<Extension>]
    static member inline Yield<'msg, 'marker, 'itemType when 'itemType :> IFabComboBoxItem>
        (_: CollectionBuilder<'msg, 'marker, IFabComboBoxItem>, x: WidgetBuilder<'msg, Memo.Memoized<'itemType>>)
        : Content<'msg> =
        { Widgets = MutStackArray1.One(x.Compile()) }
