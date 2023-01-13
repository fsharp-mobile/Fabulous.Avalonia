﻿namespace Fabulous.Avalonia.Tests

open Fabulous
open FsCheck
open NUnit.Framework
open FsUnit
open FsCheck.NUnit
open Fabulous.Avalonia

[<TestFixture>]
type ViewHelpers() =
    [<Test>]
    member _.``Existing Avalonia control can be reused if previous and current widgets are of the same type``() =
        for widgetKey in Setup.RegisteredWidgets do
            let def = WidgetDefinitionStore.get widgetKey
            
            let prev: Widget =
                { Key = widgetKey
                  DebugName = def.Name
                  ScalarAttributes = ValueNone
                  WidgetAttributes = ValueNone
                  WidgetCollectionAttributes = ValueNone }
                
            let curr: Widget =
                { Key = widgetKey
                  DebugName = def.Name
                  ScalarAttributes = ValueNone
                  WidgetAttributes = ValueNone
                  WidgetCollectionAttributes = ValueNone }
            
            let actual = ViewHelpers.canReuseView prev curr
            
            actual |> should equal true
        
    [<Property>]
    member _.``Existing TextBlock control can not be reused if previous widget uses Text and current widget uses Inlines``() =
        let arb = Arb.fromGen Generators.nonNullString
        
        Prop.forAll arb (fun text ->
            let prev =
                { Key = TextBlock.WidgetKey
                  DebugName = "TextBlock"
                  ScalarAttributes =
                      ValueSome [|
                          { Key = TextBlock.Text.Key
                            DebugName = TextBlock.Text.Name
                            Value = text
                            NumericValue = 0uL }
                      |]
                  WidgetAttributes = ValueNone
                  WidgetCollectionAttributes = ValueNone }
                
            let curr =
                { Key = TextBlock.WidgetKey
                  DebugName = "TextBlock"
                  ScalarAttributes = ValueNone
                  WidgetAttributes = ValueNone
                  WidgetCollectionAttributes =
                      ValueSome [|
                          { Key = TextBlock.Inlines.Key
                            DebugName = TextBlock.Inlines.Name
                            Value = ArraySlice.emptyWithNull () }
                      |] }
            
            let actual = ViewHelpers.canReuseView prev curr
            
            actual |> should equal false
        )
        
    [<Property>]
    member _.``Existing TextBlock control can not be reused if previous widget uses Inlines and current widget uses Text``() =
        let arb = Arb.fromGen Generators.nonNullString
        
        Prop.forAll arb (fun text ->
            let prev =
                { Key = TextBlock.WidgetKey
                  DebugName = "TextBlock"
                  ScalarAttributes = ValueNone
                  WidgetAttributes = ValueNone
                  WidgetCollectionAttributes =
                      ValueSome [|
                          { Key = TextBlock.Inlines.Key
                            DebugName = TextBlock.Inlines.Name
                            Value = ArraySlice.emptyWithNull () }
                      |] }
                
            let curr =
                { Key = TextBlock.WidgetKey
                  DebugName = "TextBlock"
                  ScalarAttributes =
                      ValueSome [|
                          { Key = TextBlock.Text.Key
                            DebugName = TextBlock.Text.Name
                            Value = text
                            NumericValue = 0uL }
                      |]
                  WidgetAttributes = ValueNone
                  WidgetCollectionAttributes = ValueNone }
            
            let actual = ViewHelpers.canReuseView prev curr
            
            actual |> should equal false
        )
        