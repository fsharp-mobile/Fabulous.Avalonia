namespace Fabulous.Avalonia.Components

open Avalonia.Controls.Documents
open Avalonia.Media
open Fabulous.Avalonia

type IFabComponentInline =
    inherit IFabComponentTextElement
    inherit IFabInline

module ComponentInline =
    let TextDecorations =
        ComponentAttributes.defineAvaloniaListWidgetCollection "Inline_TextDecorations" (fun target ->
            let target = target :?> Inline

            if target.TextDecorations = null then
                let newColl = TextDecorationCollection()
                target.TextDecorations <- newColl
                newColl
            else
                target.TextDecorations)
