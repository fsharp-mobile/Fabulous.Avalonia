namespace Fabulous.Avalonia.Mvu

open System.Runtime.CompilerServices
open Avalonia.Input
open Avalonia.Input.TextInput
open Avalonia.Interactivity
open Fabulous
open Fabulous.Avalonia

type IFabMvuInputElement =
    inherit IFabMvuInteractive
    inherit IFabInputElement

module MvuInputElement =

    let GotFocus =
        MvuAttributes.defineEvent<GotFocusEventArgs> "InputElement_GotFocus" (fun target -> (target :?> InputElement).GotFocus)

    let LostFocus =
        MvuAttributes.defineEvent<RoutedEventArgs> "InputElement_LostFocus" (fun target -> (target :?> InputElement).LostFocus)

    let KeyDown =
        MvuAttributes.defineEvent<KeyEventArgs> "InputElement_KeyDown" (fun target -> (target :?> InputElement).KeyDown)

    let KeyUp =
        MvuAttributes.defineEvent<KeyEventArgs> "InputElement_KeyUp" (fun target -> (target :?> InputElement).KeyUp)

    let TextInput =
        MvuAttributes.defineEvent<TextInputEventArgs> "InputElement_TextInput" (fun target -> (target :?> InputElement).TextInput)

    let TextInputMethodClientRequested =
        MvuAttributes.defineEvent<TextInputMethodClientRequestedEventArgs> "InputElement_TextInputMethodClientRequested" (fun target ->
            (target :?> InputElement).TextInputMethodClientRequested)

    let PointerEntered =
        MvuAttributes.defineEvent<PointerEventArgs> "InputElement_PointerEntered" (fun target -> (target :?> InputElement).PointerEntered)

    let PointerExited =
        MvuAttributes.defineEvent<PointerEventArgs> "InputElement_PointerExited" (fun target -> (target :?> InputElement).PointerExited)

    let PointerMoved =
        MvuAttributes.defineEvent<PointerEventArgs> "InputElement_PointerMoved" (fun target -> (target :?> InputElement).PointerMoved)

    let PointerPressed =
        MvuAttributes.defineEvent<PointerPressedEventArgs> "InputElement_PointerPressed" (fun target -> (target :?> InputElement).PointerPressed)

    let PointerReleased =
        MvuAttributes.defineEvent<PointerReleasedEventArgs> "InputElement_PointerReleased" (fun target -> (target :?> InputElement).PointerReleased)

    let PointerCaptureLost =
        MvuAttributes.defineEvent<PointerCaptureLostEventArgs> "InputElement_PointerCaptureLost" (fun target ->
            (target :?> InputElement).PointerCaptureLost)

    let PointerWheelChanged =
        MvuAttributes.defineEvent<PointerWheelEventArgs> "InputElement_PointerWheelChanged" (fun target -> (target :?> InputElement).PointerWheelChanged)

    let Tapped =
        MvuAttributes.defineEvent<TappedEventArgs> "InputElement_Tapped" (fun target -> (target :?> InputElement).Tapped)

    let Holding =
        MvuAttributes.defineEvent<HoldingRoutedEventArgs> "InputElement_Holding" (fun target -> (target :?> InputElement).Holding)

    let DoubleTapped =
        MvuAttributes.defineEvent<TappedEventArgs> "InputElement_DoubleTapped" (fun target -> (target :?> InputElement).DoubleTapped)

type MvuInputElementModifiers =
    /// <summary>Listens to the InputElement GotFocus event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when control receives focus.</param>
    [<Extension>]
    static member inline onGotFocus(this: WidgetBuilder<unit, #IFabMvuInputElement>, fn: GotFocusEventArgs -> unit) =
        this.AddScalar(MvuInputElement.GotFocus.WithValue(fn))

    /// <summary>Listens to the InputElement LostFocus event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when control loses focus.</param>
    [<Extension>]
    static member inline onLostFocus(this: WidgetBuilder<unit, #IFabMvuInputElement>, fn: RoutedEventArgs -> unit) =
        this.AddScalar(MvuInputElement.LostFocus.WithValue(fn))

    /// <summary>Listens to the InputElement KeyDown event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when a key is pressed while the control has focus.</param>
    [<Extension>]
    static member inline onKeyDown(this: WidgetBuilder<unit, #IFabMvuInputElement>, fn: KeyEventArgs -> unit) =
        this.AddScalar(MvuInputElement.KeyDown.WithValue(fn))

    /// <summary>Listens to the InputElement KeyUp event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when a key is released while the control has focus.</param>
    [<Extension>]
    static member inline onKeyUp(this: WidgetBuilder<unit, #IFabMvuInputElement>, fn: KeyEventArgs -> unit) =
        this.AddScalar(MvuInputElement.KeyUp.WithValue(fn))

    /// <summary>Listens to the InputElement TextInput event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when a user typed some text while the control has focus.</param>
    [<Extension>]
    static member inline onTextInput(this: WidgetBuilder<unit, #IFabMvuInputElement>, fn: TextInputEventArgs -> unit) =
        this.AddScalar(MvuInputElement.TextInput.WithValue(fn))

    /// <summary>Listens to the InputElement TextInputMethodClientRequested event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when an input element gains input focus and input method is looking for the corresponding client.</param>
    [<Extension>]
    static member inline onTextInputMethodClientRequested
        (this: WidgetBuilder<unit, #IFabMvuInputElement>, fn: TextInputMethodClientRequestedEventArgs -> unit)
        =
        this.AddScalar(MvuInputElement.TextInputMethodClientRequested.WithValue(fn))

    /// <summary>Listens to the InputElement PointerEntered event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when pointer enters the control.</param>
    [<Extension>]
    static member inline onPointerEntered(this: WidgetBuilder<unit, #IFabMvuInputElement>, fn: PointerEventArgs -> unit) =
        this.AddScalar(MvuInputElement.PointerEntered.WithValue(fn))

    /// <summary>Listens to the InputElement PointerExited event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when pointer leaves the control.</param>
    [<Extension>]
    static member inline onPointerExited(this: WidgetBuilder<unit, #IFabMvuInputElement>, fn: PointerEventArgs -> unit) =
        this.AddScalar(MvuInputElement.PointerExited.WithValue(fn))

    /// <summary>Listens to the InputElement PointerMoved event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when pointer moves over the control.</param>
    [<Extension>]
    static member inline onPointerMoved(this: WidgetBuilder<unit, #IFabMvuInputElement>, fn: PointerEventArgs -> unit) =
        this.AddScalar(MvuInputElement.PointerMoved.WithValue(fn))

    /// <summary>Listens to the InputElement PointerPressed event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the pointer is pressed over the control.</param>
    [<Extension>]
    static member inline onPointerPressed(this: WidgetBuilder<unit, #IFabMvuInputElement>, fn: PointerPressedEventArgs -> unit) =
        this.AddScalar(MvuInputElement.PointerPressed.WithValue(fn))

    /// <summary>Listens to the InputElement PointerReleased event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the pointer is released over the control.</param>
    [<Extension>]
    static member inline onPointerReleased(this: WidgetBuilder<unit, #IFabMvuInputElement>, fn: PointerReleasedEventArgs -> unit) =
        this.AddScalar(MvuInputElement.PointerReleased.WithValue(fn))

    /// <summary>Listens to the InputElement PointerCaptureLost event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the control or its child control loses the pointer capture for any reason event will not be triggered for a parent control if capture was transferred to another child of that parent control.</param>
    [<Extension>]
    static member inline onPointerCaptureLost(this: WidgetBuilder<unit, #IFabMvuInputElement>, fn: PointerCaptureLostEventArgs -> unit) =
        this.AddScalar(MvuInputElement.PointerCaptureLost.WithValue(fn))

    /// <summary>Listens to the InputElement PointerWheelChanged event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the pointer wheel changes.</param>
    [<Extension>]
    static member inline onPointerWheelChanged(this: WidgetBuilder<unit, #IFabMvuInputElement>, fn: PointerWheelEventArgs -> unit) =
        this.AddScalar(MvuInputElement.PointerWheelChanged.WithValue(fn))

    /// <summary>Listens to the InputElement Tapped event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when a tap gesture occurs on the control.</param>
    [<Extension>]
    static member inline onTapped(this: WidgetBuilder<unit, #IFabMvuInputElement>, fn: RoutedEventArgs -> unit) =
        this.AddScalar(MvuInputElement.Tapped.WithValue(fn))

    /// <summary>Listens to the InputElement Holding event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when a holding gesture occurs on the control.</param>
    [<Extension>]
    static member inline onHolding(this: WidgetBuilder<unit, #IFabMvuInputElement>, fn: HoldingRoutedEventArgs -> unit) =
        this.AddScalar(MvuInputElement.Holding.WithValue(fn))

    /// <summary>Listens to the InputElement RightTapped event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when a double-tap gesture occurs on the control.</param>
    [<Extension>]
    static member inline onDoubleTapped(this: WidgetBuilder<unit, #IFabMvuInputElement>, fn: RoutedEventArgs -> unit) =
        this.AddScalar(MvuInputElement.DoubleTapped.WithValue(fn))
