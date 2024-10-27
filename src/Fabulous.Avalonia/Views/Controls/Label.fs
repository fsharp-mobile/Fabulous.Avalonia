namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls
open Avalonia.Input
open Fabulous

type IFabLabel =
    inherit IFabContentControl

module Label =
    let WidgetKey = Widgets.register<Label>()

    let Target = Attributes.defineAvaloniaPropertyWithEquality Label.TargetProperty

type LabelModifiers =
    /// <summary>Sets the Target property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The Target value.</param>
    [<Extension>]
    static member inline target(this: WidgetBuilder<'msg, #IFabLabel>, value: ViewRef<#IInputElement>) =
        match value.TryValue with
        | None -> this
        | Some value -> this.AddScalar(Label.Target.WithValue(value))

    /// <summary>Link a ViewRef to access the direct Label control instance.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control.</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, #IFabLabel>, value: ViewRef<Label>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
