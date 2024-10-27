namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls
open Fabulous

type IFabSeparator =
    inherit IFabTemplatedControl

module Separator =
    let WidgetKey = Widgets.register<Separator>()

type SeparatorModifiers =
    /// <summary>Link a ViewRef to access the direct Separator control instance.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control.</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabSeparator>, value: ViewRef<Separator>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
