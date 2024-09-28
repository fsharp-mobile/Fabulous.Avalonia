namespace Fabulous.Avalonia.Mvu

open System.Globalization
open System.Runtime.CompilerServices
open Avalonia.Controls
open Fabulous
open Fabulous.Avalonia

type IFabMvuMaskedTextBox =
    inherit IFabMvuTextBox
    inherit IFabMaskedTextBox

module MvuMaskedTextBox =
    let TextChanged =
        MvuAttributes.defineAvaloniaPropertyWithChangedEvent' "MaskedTextBox_TextChanged" MaskedTextBox.TextProperty

[<AutoOpen>]
module MvuMaskedTextBoxBuilders =
    type Fabulous.Avalonia.Mvu.View with

        /// <summary>Creates a MaskedTextBox widget.</summary>
        /// <param name="text">The text to display.</param>
        /// <param name="mask">The mask to apply.</param>
        /// <param name="fn">Raised when the text changes.</param>
        static member inline MaskedTextBox(text: string, mask: string, fn: string -> unit) =
            WidgetBuilder<unit, IFabMvuMaskedTextBox>(
                MaskedTextBox.WidgetKey,
                MaskedTextBox.Mask.WithValue(mask),
                MvuMaskedTextBox.TextChanged.WithValue(MvuValueEventData.create text fn)
            )

type MvuMaskedTextBoxModifiers =
    /// <summary>Link a ViewRef to access the direct MaskedTextBox control instance.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control.</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<unit, IFabMvuMaskedTextBox>, value: ViewRef<MaskedTextBox>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
