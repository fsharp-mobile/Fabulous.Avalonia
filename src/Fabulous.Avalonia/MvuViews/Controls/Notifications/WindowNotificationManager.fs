namespace Fabulous.Avalonia.Mvu

open Avalonia.Controls.Notifications
open Fabulous
open Fabulous.Avalonia
open Fabulous.StackAllocatedCollections.StackList

type IFabMvuWindowNotificationManager =
    inherit IFabMvuTemplatedControl
    inherit IFabWindowNotificationManager

[<AutoOpen>]
module MvuWindowNotificationManagerBuilders =
    type Fabulous.Avalonia.Mvu.View with

        /// <summary>Creates a WindowNotificationManager widget.</summary>
        static member WindowNotificationManager(viewRef: ViewRef<WindowNotificationManager>) =
            WidgetBuilder<'msg, IFabMvuWindowNotificationManager>(
                WindowNotificationManager.WidgetKey,
                AttributesBundle(StackList.one(ViewRefAttributes.ViewRef.WithValue(viewRef.Unbox)), ValueNone, ValueNone)
            )
