namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls
open Avalonia.Media
open Fabulous

type IFabViewBox =
    inherit IFabControl

module ViewBox =
    let WidgetKey = Widgets.register<Viewbox>()

    let Stretch = Attributes.defineAvaloniaPropertyWithEquality Viewbox.StretchProperty

    let StretchDirection =
        Attributes.defineAvaloniaPropertyWithEquality Viewbox.StretchDirectionProperty

    let Child = Attributes.defineAvaloniaPropertyWidget Viewbox.ChildProperty

type ViewBoxModifiers =
    /// <summary>Sets the Stretch property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The Stretch value.</param>
    [<Extension>]
    static member inline stretch(this: WidgetBuilder<'msg, #IFabViewBox>, value: Stretch) =
        this.AddScalar(ViewBox.Stretch.WithValue(value))

    /// <summary>Sets the StretchDirection property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The StretchDirection value.</param>
    [<Extension>]
    static member inline stretchDirection(this: WidgetBuilder<'msg, #IFabViewBox>, value: StretchDirection) =
        this.AddScalar(ViewBox.StretchDirection.WithValue(value))

    /// <summary>Link a ViewRef to access the direct ViewBox control instance.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control.</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabViewBox>, value: ViewRef<Viewbox>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
