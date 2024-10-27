namespace Fabulous.Avalonia.Mvu

open Avalonia
open Fabulous
open Fabulous.Avalonia

type IFabMvuLinearGradientBrush =
    inherit IFabMvuGradientBrush
    inherit IFabLinearGradientBrush

[<AutoOpen>]
module MvuLinearGradientBrushBuilders =
    type Fabulous.Avalonia.Mvu.View with

        /// <summary>Creates a LinearGradientBrush widget.</summary>
        /// <param name="startPoint">The start point of the gradient.</param>
        /// <param name="endPoint">The end point of the gradient.</param>
        static member LinearGradientBrush(startPoint: RelativePoint, endPoint: RelativePoint) =
            CollectionBuilder<'msg, IFabMvuLinearGradientBrush, IFabMvuGradientStop>(
                LinearGradientBrush.WidgetKey,
                MvuGradientBrush.GradientStops,
                LinearGradientBrush.StartPoint.WithValue(startPoint),
                LinearGradientBrush.EndPoint.WithValue(endPoint)
            )

        /// <summary>Creates a LinearGradientBrush widget.</summary>
        /// <param name="startPoint">The start point of the gradient.</param>
        /// <param name="endPoint">The end point of the gradient.</param>
        static member LinearGradientBrush'(startPoint: RelativePoint, endPoint: RelativePoint) =
            WidgetBuilder<'msg, IFabMvuLinearGradientBrush>(
                LinearGradientBrush.WidgetKey,
                LinearGradientBrush.StartPoint.WithValue(startPoint),
                LinearGradientBrush.EndPoint.WithValue(endPoint)
            )

        /// <summary>Creates a LinearGradientBrush widget.</summary>
        /// <param name="startPoint">The start point of the gradient.</param>
        /// <param name="endPoint">The end point of the gradient.</param>
        /// <param name="unit">The relative 'msg of the start and end points.</param>
        static member LinearGradientBrush(startPoint: Point, endPoint: Point, unit: RelativeUnit) =
            CollectionBuilder<'msg, IFabMvuLinearGradientBrush, IFabMvuGradientStop>(
                LinearGradientBrush.WidgetKey,
                MvuGradientBrush.GradientStops,
                LinearGradientBrush.StartPoint.WithValue(RelativePoint(startPoint, unit)),
                LinearGradientBrush.EndPoint.WithValue(RelativePoint(endPoint, unit))
            )

        /// <summary>Creates a LinearGradientBrush widget.</summary>
        /// <param name="startPoint">The start point of the gradient.</param>
        /// <param name="endPoint">The end point of the gradient.</param>
        static member LinearGradientBrush(startPoint: Point, endPoint: Point) =
            let startPoint = RelativePoint(startPoint, RelativeUnit.Relative)
            let endPoint = RelativePoint(endPoint, RelativeUnit.Relative)
            View.LinearGradientBrush(startPoint, endPoint)

        /// <summary>Creates a LinearGradientBrush widget.</summary>
        /// <param name="startPoint">The start point of the gradient.</param>
        /// <param name="endPoint">The end point of the gradient.</param>
        /// <param name="startUnit">The relative 'msg of the start point.</param>
        /// <param name="endUnit">The relative 'msg of the end point.</param>
        static member LinearGradientBrush(startPoint: Point, endPoint: Point, startUnit: RelativeUnit, endUnit: RelativeUnit) =
            CollectionBuilder<'msg, IFabMvuLinearGradientBrush, IFabMvuGradientStop>(
                LinearGradientBrush.WidgetKey,
                MvuGradientBrush.GradientStops,
                LinearGradientBrush.StartPoint.WithValue(RelativePoint(startPoint, startUnit)),
                LinearGradientBrush.EndPoint.WithValue(RelativePoint(endPoint, endUnit))
            )

        /// <summary>Creates a LinearGradientBrush widget.</summary>
        static member LinearGradientBrush() =
            CollectionBuilder<'msg, IFabMvuLinearGradientBrush, IFabMvuGradientStop>(
                LinearGradientBrush.WidgetKey,
                MvuGradientBrush.GradientStops,
                LinearGradientBrush.StartPoint.WithValue(RelativePoint.TopLeft),
                LinearGradientBrush.EndPoint.WithValue(RelativePoint.BottomRight)
            )

        /// <summary>Creates a LinearGradientBrush widget.</summary>
        static member LinearGradientBrush'() =
            WidgetBuilder<'msg, IFabMvuLinearGradientBrush>(
                LinearGradientBrush.WidgetKey,
                LinearGradientBrush.StartPoint.WithValue(RelativePoint.TopLeft),
                LinearGradientBrush.EndPoint.WithValue(RelativePoint.BottomRight)
            )
