namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls
open Avalonia.Styling
open Fabulous
open Fabulous.Avalonia
open Fabulous.StackAllocatedCollections.StackList

module MvuThemeVariantScope =
    let ActualThemeVariantChanged =
        Attributes.defineEventNoArg "TopLevel_ThemeVariantChanged" (fun target -> (target :?> ThemeVariantScope).ActualThemeVariantChanged)

[<AutoOpen>]
module MvuThemeVariantScopeBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a ThemeVariantScope widget.</summary>
        /// <param name="theme">The theme variant to use.</param>
        /// <param name="content">The content of the ThemeVariantScope.</param>
        static member ThemeVariantScope(theme: ThemeVariant, content: WidgetBuilder<'msg, #IFabControl>) =
            WidgetBuilder<'msg, IFabThemeVariantScope>(
                ThemeVariantScope.WidgetKey,
                AttributesBundle(
                    StackList.one(ThemeVariantScope.RequestedThemeVariant.WithValue(theme)),
                    ValueSome [| Decorator.ChildWidget.WithValue(content.Compile()) |],
                    ValueNone
                )
            )

type MvuThemeVariantScopeModifiers =

    /// <summary>Listens the ThemeVariantScope ThemeVariantChanged event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the ThemeVariantChanged event is raised.</param>
    [<Extension>]
    static member inline onActualThemeVariantChanged(this: WidgetBuilder<'msg, #IFabThemeVariantScope>, fn: 'msg) =
        this.AddScalar(MvuThemeVariantScope.ActualThemeVariantChanged.WithValue(MsgValue fn))