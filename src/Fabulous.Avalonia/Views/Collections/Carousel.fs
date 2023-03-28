namespace Fabulous.Avalonia

open System
open System.Collections.Generic
open System.Runtime.CompilerServices
open Avalonia.Animation
open Avalonia.Controls
open Avalonia.Styling
open Fabulous

type IFabCarousel =
    inherit IFabSelectingItemsControl

type CarouselController() =
    let next = Event<EventHandler, EventArgs>()
    let previous = Event<EventHandler, EventArgs>()

    [<CLIEvent>]
    member _.Next = next.Publish

    [<CLIEvent>]
    member _.Previous = previous.Publish

    member this.DoNext() = next.Trigger(this, EventArgs.Empty)

    member this.DoPrevious() = previous.Trigger(this, EventArgs.Empty)

type CustomCarousel() as this =
    inherit Carousel()

    let _nextHandler = EventHandler(this.CustomNext)

    let _previousHandler = EventHandler(this.CustomPrevious)

    let mutable _controller: CarouselController option = None

    member this.Controller
        with get () = _controller
        and set value =
            if _controller <> value then
                // Unsubscribe the old controller
                if _controller.IsSome then
                    _controller.Value.Next.RemoveHandler(this.CustomNext)
                    _controller.Value.Previous.RemoveHandler(this.CustomPrevious)

                // Subscribe the new controller
                _controller <- value
                _controller.Value.Next.AddHandler(this.CustomNext)
                _controller.Value.Previous.AddHandler(this.CustomPrevious)

    member private this.CustomNext _ _ = this.Next()

    member private this.CustomPrevious _ _ = this.Previous()

    interface IStyleable with
        member this.StyleKey = typeof<Carousel>

module Carousel =
    let WidgetKey = Widgets.register<CustomCarousel>()

    let Controller =
        Attributes.defineProperty "Carousel_Controller" None (fun target value -> (target :?> CustomCarousel).Controller <- value)

    let PageTransition =
        Attributes.defineAvaloniaPropertyWithEquality Carousel.PageTransitionProperty

[<AutoOpen>]
module CarouselBuilders =
    type Fabulous.Avalonia.View with

        static member inline Carousel<'msg, 'itemData, 'itemMarker when 'itemMarker :> IFabControl>
            (
                items: seq<'itemData>,
                template: 'itemData -> WidgetBuilder<'msg, 'itemMarker>
            ) =
            WidgetHelpers.buildItems<'msg, IFabCarousel, 'itemData, 'itemMarker> Carousel.WidgetKey ItemsControl.ItemsSource items template

        static member inline Carousel<'msg>() =
            CollectionBuilder<'msg, IFabCarousel, IFabControl>(Carousel.WidgetKey, ItemsControl.Items)

[<Extension>]
type CarouselModifiers =

    [<Extension>]
    static member inline transition(this: WidgetBuilder<'msg, #IFabCarousel>, value: IPageTransition) =
        this.AddScalar(Carousel.PageTransition.WithValue(value))

    [<Extension>]
    static member inline controller(this: WidgetBuilder<'msg, #IFabCarousel>, value: CarouselController) =
        this.AddScalar(Carousel.Controller.WithValue(Some value))
