namespace Fabulous.Avalonia.Mvu

open System
open System.Runtime.CompilerServices
open Avalonia.Controls
open Avalonia.Layout
open Fabulous
open Fabulous.Avalonia

type IFabMvuCalendarDatePicker =
    inherit IFabMvuTemplatedControl
    inherit IFabCalendarDatePicker

module MvuCalendarDatePicker =
    let SelectedDateChanged =
        MvuAttributes.defineAvaloniaPropertyWithChangedEvent
            "CalendarDatePicker_SelectedDateChanged"
            CalendarDatePicker.SelectedDateProperty
            Option.toNullable
            Option.ofNullable

    let DateValidationError =
        MvuAttributes.defineEvent "CalendarDatePicker_DateValidationError" (fun target -> (target :?> CalendarDatePicker).DateValidationError)

    let CalendarClosed =
        MvuAttributes.defineEventNoArg "CalendarDatePicker_CalendarClosed" (fun target -> (target :?> CalendarDatePicker).CalendarClosed)

    let CalendarOpened =
        MvuAttributes.defineEventNoArg "CalendarDatePicker_CalendarOpened" (fun target -> (target :?> CalendarDatePicker).CalendarOpened)

[<AutoOpen>]
module MvuCalendarDatePickerBuilders =
    type Fabulous.Avalonia.Mvu.View with

        /// <summary>Creates a CalendarDatePicker widget.</summary>
        /// <param name="date">The selected date.</param>
        /// <param name="fn">Raised when the selected date changes.</param>
        static member CalendarDatePicker(date: DateTime option, fn: DateTime option -> unit) =
            WidgetBuilder<unit, IFabCalendarDatePicker>(
                CalendarDatePicker.WidgetKey,
                MvuCalendarDatePicker.SelectedDateChanged.WithValue(MvuValueEventData.create date fn)
            )

type MvuCalendarDatePickerModifiers =
    /// <summary>Listens to the CalendarDatePicker DateValidationError event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the DatePicker detects a format error.</param>
    [<Extension>]
    static member inline onDateValidationError(this: WidgetBuilder<unit, #IFabCalendarDatePicker>, fn: CalendarDatePickerDateValidationErrorEventArgs -> unit) =
        this.AddScalar(MvuCalendarDatePicker.DateValidationError.WithValue(fn))

    /// <summary>Listens to the CalendarDatePicker CalendarClosed event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the DatePicker closes its calendar.</param>
    [<Extension>]
    static member inline onCalendarClosed(this: WidgetBuilder<unit, #IFabCalendarDatePicker>, fn: unit -> unit) =
        this.AddScalar(MvuCalendarDatePicker.CalendarClosed.WithValue(fn))

    /// <summary>Listens to the CalendarDatePicker CalendarOpened event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the DatePicker opens its calendar.</param>
    [<Extension>]
    static member inline onCalendarOpened(this: WidgetBuilder<unit, #IFabCalendarDatePicker>, fn: unit -> unit) =
        this.AddScalar(MvuCalendarDatePicker.CalendarOpened.WithValue(fn))

    /// <summary>Link a ViewRef to access the direct CalendarDatePicker control instance.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control.</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<unit, IFabCalendarDatePicker>, value: ViewRef<CalendarDatePicker>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
