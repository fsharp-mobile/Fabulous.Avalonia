namespace Fabulous.Avalonia

open System.Collections
open System.ComponentModel
open Avalonia
open Avalonia.Collections
open Avalonia.Interactivity
open Fabulous
open Fabulous.ScalarAttributeDefinitions

[<Struct>]
type ComponentValueEventData<'data, 'eventArgs> =
    { Value: 'data voption
      Event: 'eventArgs -> unit }

module ComponentValueEventData =
    let create (value: 'data) (event: 'eventArgs -> unit) =
        { Value = ValueSome value
          Event = event }

    let createVOption (value: 'data voption) (event: 'eventArgs -> unit) =
        { Value = value
          Event = event }

module ComponentAttributes =
    let defineAvaloniaPropertyWithChangedEvent<'modelType, 'valueType>
        name
        (property: AvaloniaProperty<'valueType>)
        (convertToValue: 'modelType -> 'valueType)
        (convertToModel: 'valueType -> 'modelType)
        : SimpleScalarAttributeDefinition<ComponentValueEventData<'modelType, 'modelType>> =

        let key =
            SimpleScalarAttributeDefinition.CreateAttributeData(
                ScalarAttributeComparers.noCompare,
                (fun oldValueOpt (newValueOpt: ComponentValueEventData<'modelType, 'modelType> voption) node ->
                    let target = node.Target :?> AvaloniaObject

                    // The attribute is no longer applied, so we clean up the event
                    match node.TryGetHandler(property.Name) with
                    | ValueNone -> ()
                    | ValueSome handler -> handler.Dispose()

                    match newValueOpt with
                    | ValueNone ->
                        match oldValueOpt with
                        | ValueNone -> ()
                        | ValueSome _ -> target.ClearValue(property)

                    | ValueSome curr ->
                        // Clean up the old event handler if any
                        match node.TryGetHandler(property.Name) with
                        | ValueNone -> ()
                        | ValueSome handler -> handler.Dispose()

                        // Set the new value

                        match curr.Value with
                        | ValueNone -> ()
                        | ValueSome v ->
                            let newValue = convertToValue v
                            target.SetValue(property, box newValue) |> ignore

                        let event = property.Changed
                        // Set the new event handler
                        let disposable =
                            event.Subscribe(fun args ->
                                if args.Sender = target then
                                    if args.NewValue.HasValue then
                                        curr.Event (convertToModel args.NewValue.Value))

                        node.SetHandler(property.Name, disposable))
            )
            |> AttributeDefinitionStore.registerScalar

        { Key = key; Name = name }

    let defineAvaloniaPropertyWithChangedEvent'<'T> name (property: AvaloniaProperty<'T>) : SimpleScalarAttributeDefinition<ComponentValueEventData<'T, 'T>> =
        defineAvaloniaPropertyWithChangedEvent<'T, 'T> name property id id

    let defineRoutedEvent<'args when 'args :> RoutedEventArgs> name (property: RoutedEvent<'args>) : SimpleScalarAttributeDefinition<'args -> unit> =
        let key =
            SimpleScalarAttributeDefinition.CreateAttributeData(
                ScalarAttributeComparers.noCompare,
                (fun _ (newValueOpt: ('args -> unit) voption) (node: IViewNode) ->
                    match node.TryGetHandler(name) with
                    | ValueNone -> ()
                    | ValueSome handler -> handler.Dispose()

                    match newValueOpt with
                    | ValueNone -> node.Dispose()

                    | ValueSome fn ->
                        let event = property.AddClassHandler(fun _ args -> fn args |> ignore)

                        node.SetHandler(name, event))
            )
            |> AttributeDefinitionStore.registerScalar

        { Key = key; Name = name }

    let inline defineCancelEvent
        name
        ([<InlineIfLambda>] getEvent: obj -> IEvent<CancelEventHandler, CancelEventArgs>)
        : SimpleScalarAttributeDefinition<CancelEventArgs -> unit> =
        let key =
            SimpleScalarAttributeDefinition.CreateAttributeData(
                ScalarAttributeComparers.noCompare,
                (fun _ (newValueOpt: (CancelEventArgs -> unit) voption) (node: IViewNode) ->
                    match node.TryGetHandler(name) with
                    | ValueNone -> ()
                    | ValueSome handler -> handler.Dispose()

                    match newValueOpt with
                    | ValueNone -> node.Dispose()

                    | ValueSome fn ->
                        let event = getEvent node.Target

                        let handler = event.Subscribe(fun args -> fn args |> ignore)

                        node.SetHandler(name, handler))
            )
            |> AttributeDefinitionStore.registerScalar

        { Key = key; Name = name }

    /// Define an attribute storing a collection of Widget for a List<T> property
    let inline defineAvaloniaNonGenericListWidgetCollection name ([<InlineIfLambda>] getCollection: obj -> IList) =
        let applyDiff _ (diffs: WidgetCollectionItemChanges) (node: IViewNode) =
            let targetColl = getCollection node.Target

            for diff in diffs do
                match diff with
                | WidgetCollectionItemChange.Remove(index, widget) ->
                    let itemNode = node.TreeContext.GetViewNode(targetColl[index])

                    // // Trigger the unmounted event
                    Dispatcher.dispatchEventForAllChildren itemNode widget Lifecycle.Unmounted
                    itemNode.Dispose()

                    // Remove the child from the UI tree
                    targetColl.RemoveAt(index)

                | _ -> ()

            for diff in diffs do
                match diff with
                | WidgetCollectionItemChange.Insert(index, widget) ->
                    let struct (itemNode, view) = Helpers.createViewForWidget node widget

                    // Insert the new child into the UI tree
                    targetColl.Insert(index, unbox view)

                // Trigger the mounted event
                // FIXME Dispatcher.dispatchEventForAllChildren' itemNode widget ComponentLifecycle.Mounted

                | WidgetCollectionItemChange.Update(index, widgetDiff) ->
                    let childNode = node.TreeContext.GetViewNode(targetColl[index])

                    childNode.ApplyDiff(&widgetDiff)

                | WidgetCollectionItemChange.Replace(index, oldWidget, newWidget) ->
                    let prevItemNode = node.TreeContext.GetViewNode(targetColl[index])

                    let struct (nextItemNode, view) = Helpers.createViewForWidget node newWidget

                    // Trigger the unmounted event for the old child
                    // FIXME Dispatcher.dispatchEventForAllChildren' prevItemNode oldWidget ComponentLifecycle.Unmounted
                    prevItemNode.Dispose()

                    // Replace the existing child in the UI tree at the index with the new one
                    targetColl[index] <- view

                // Trigger the mounted event for the new child
                // FIXME Dispatcher.dispatchEventForAllChildren' nextItemNode newWidget ComponentLifecycle.Mounted

                | _ -> ()

        let updateNode _ (newValueOpt: ArraySlice<Widget> voption) (node: IViewNode) =
            let targetColl = getCollection node.Target
            targetColl.Clear()

            match newValueOpt with
            | ValueNone -> ()
            | ValueSome widgets ->
                for widget in ArraySlice.toSpan widgets do
                    let struct (_, view) = Helpers.createViewForWidget node widget

                    targetColl.Add(view) |> ignore

        Attributes.defineWidgetCollection name applyDiff updateNode

    /// Define an attribute storing a collection of Widget for a AvaloniaList<T> property
    let defineAvaloniaListWidgetCollection<'itemType> name (getCollection: obj -> IAvaloniaList<'itemType>) =
        let applyDiff _ (diffs: WidgetCollectionItemChanges) (node: IViewNode) =
            let targetColl = getCollection node.Target

            for diff in diffs do
                match diff with
                | WidgetCollectionItemChange.Remove(index, widget) ->
                    let itemNode = node.TreeContext.GetViewNode(box targetColl[index])

                    // Trigger the unmounted event
                    // FIXME Dispatcher.dispatchEventForAllChildren' itemNode widget ComponentLifecycle.Unmounted
                    itemNode.Dispose()

                    // Remove the child from the UI tree
                    targetColl.RemoveAt(index)

                | _ -> ()

            for diff in diffs do
                match diff with
                | WidgetCollectionItemChange.Insert(index, widget) ->
                    let struct (itemNode, view) = Helpers.createViewForWidget node widget

                    // Insert the new child into the UI tree
                    targetColl.Insert(index, unbox view)

                // Trigger the mounted event
                // FIXME Dispatcher.dispatchEventForAllChildren' itemNode widget ComponentLifecycle.Mounted

                | WidgetCollectionItemChange.Update(index, widgetDiff) ->
                    let childNode = node.TreeContext.GetViewNode(box targetColl[index])

                    childNode.ApplyDiff(&widgetDiff)

                | WidgetCollectionItemChange.Replace(index, oldWidget, newWidget) ->
                    let prevItemNode = node.TreeContext.GetViewNode(box targetColl[index])

                    let struct (nextItemNode, view) = Helpers.createViewForWidget node newWidget

                    // Trigger the unmounted event for the old child
                    // FIXME Dispatcher.dispatchEventForAllChildren' prevItemNode oldWidget ComponentLifecycle.Unmounted
                    prevItemNode.Dispose()

                    // Replace the existing child in the UI tree at the index with the new one
                    targetColl[index] <- unbox view

                // Trigger the mounted event for the new child
                // FIXME Dispatcher.dispatchEventForAllChildren' nextItemNode newWidget ComponentLifecycle.Mounted

                | _ -> ()

        let updateNode _ (newValueOpt: ArraySlice<Widget> voption) (node: IViewNode) =
            let targetColl = getCollection node.Target
            targetColl.Clear()

            match newValueOpt with
            | ValueNone -> ()
            | ValueSome widgets ->
                for widget in ArraySlice.toSpan widgets do
                    let struct (_, view) = Helpers.createViewForWidget node widget

                    targetColl.Add(unbox view)

        Attributes.defineWidgetCollection name applyDiff updateNode
