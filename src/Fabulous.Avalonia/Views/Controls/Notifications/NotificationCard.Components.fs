namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls.Notifications
open Avalonia.Interactivity
open Fabulous
open Fabulous.Avalonia
open Fabulous.StackAllocatedCollections.StackList

module ComponentNotificationCard =
    let NotificationClosed =
        Attributes.defineEventNoDispatch "NotificationCard_NotificationClosed" (fun target -> (target :?> NotificationCard).NotificationClosed)

[<AutoOpen>]
module ComponentNotificationCardBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a NotificationCard widget.</summary>
        /// <param name="isClosed">Whether the NotificationCard is closed.</param>
        /// <param name="content">The content of the NotificationCard.</param>
        static member NotificationCard(isClosed: bool, content: WidgetBuilder<'msg, #IFabControl>) =
            WidgetBuilder<'msg, IFabNotificationCard>(
                NotificationCard.WidgetKey,
                AttributesBundle(
                    StackList.one(NotificationCard.IsClosed.WithValue(isClosed)),
                    ValueSome [| ContentControl.ContentWidget.WithValue(content.Compile()) |],
                    ValueNone
                )
            )

        /// <summary>Creates a NotificationCard widget.</summary>
        /// <param name="isClosed">Whether the NotificationCard is closed.</param>
        /// <param name="content">The content of the NotificationCard.</param>
        static member NotificationCard(isClosed: bool, content: string) =
            WidgetBuilder<'msg, IFabNotificationCard>(
                NotificationCard.WidgetKey,
                NotificationCard.IsClosed.WithValue(isClosed),
                ContentControl.ContentString.WithValue(content)
            )

type ComponentNotificationCardModifiers =
    /// <summary>Listens to the NotificationCard NotificationClosed event.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="fn">Raised when the NotificationCard is closed.</param>
    [<Extension>]
    static member inline onNotificationClosed(this: WidgetBuilder<'msg, #IFabNotificationCard>, fn: RoutedEventArgs -> unit) =
        this.AddScalar(ComponentNotificationCard.NotificationClosed.WithValue(fn))