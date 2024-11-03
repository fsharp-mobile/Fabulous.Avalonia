namespace Fabulous.Avalonia

open System
open System.Runtime.CompilerServices
open Avalonia.Controls
open Fabulous
open Fabulous.Avalonia

module ComponentCalendarDatePicker =
    let SelectedDateChanged =
        ComponentAttributes.defineAvaloniaPropertyWithChangedEvent
            "CalendarDatePicker_SelectedDateChanged"
            CalendarDatePicker.SelectedDateProperty
            Option.toNullable
            Option.ofNullable

    let DateValidationError =
        Attributes.defineEventNoDispatch "CalendarDatePicker_DateValidationError" (fun target -> (target :?> CalendarDatePicker).DateValidationError)

    let CalendarClosed =
        Attributes.defineEventNoArgNoDispatch "CalendarDatePicker_CalendarClosed" (fun target -> (target :?> CalendarDatePicker).CalendarClosed)

    let CalendarOpened =
        Attributes.defineEventNoArgNoDispatch "CalendarDatePicker_CalendarOpened" (fun target -> (target :?> CalendarDatePicker).CalendarOpened)

[<AutoOpen>]
module ComponentCalendarDatePickerBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a CalendarDatePicker widget.</summary>
        /// <param name="date">The selected date.</param>
        /// <param name="fn">Raised when the selected date changes.</param>
        static member CalendarDatePicker(date: DateTime option, fn: DateTime option -> unit) =
            WidgetBuilder<unit, IFabCalendarDatePicker>(
                CalendarDatePicker.WidgetKey,
                ComponentCalendarDatePicker.SelectedDateChanged.WithValue(ComponentValueEventData.create date fn)
            )

type ComponentCalendarDatePickerModifiers =
    /// <summary>Listens to the CalendarDatePicker DateValidationError event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the DatePicker detects a format error.</param>
    [<Extension>]
    static member inline onDateValidationError(this: WidgetBuilder<unit, #IFabCalendarDatePicker>, fn: CalendarDatePickerDateValidationErrorEventArgs -> unit) =
        this.AddScalar(ComponentCalendarDatePicker.DateValidationError.WithValue(fn))

    /// <summary>Listens to the CalendarDatePicker CalendarClosed event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the DatePicker closes its calendar.</param>
    [<Extension>]
    static member inline onCalendarClosed(this: WidgetBuilder<unit, #IFabCalendarDatePicker>, fn: unit -> unit) =
        this.AddScalar(ComponentCalendarDatePicker.CalendarClosed.WithValue(fn))

    /// <summary>Listens to the CalendarDatePicker CalendarOpened event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the DatePicker opens its calendar.</param>
    [<Extension>]
    static member inline onCalendarOpened(this: WidgetBuilder<unit, #IFabCalendarDatePicker>, fn: unit -> unit) =
        this.AddScalar(ComponentCalendarDatePicker.CalendarOpened.WithValue(fn))
