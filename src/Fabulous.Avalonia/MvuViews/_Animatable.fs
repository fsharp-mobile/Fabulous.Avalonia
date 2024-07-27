namespace Fabulous.Avalonia.Mvu

open Avalonia.Animation
open Fabulous.Avalonia

type IFabAnimatable =
    inherit IFabMvuElement
    inherit IFabElement

module Animatable =
    let Transitions =
        MvuAttributes.defineAvaloniaListWidgetCollection "Animatable_Transitions" (fun target ->
            let target = (target :?> Animatable)

            if target.Transitions = null then
                let newColl = Transitions()
                target.Transitions <- newColl
                newColl
            else
                target.Transitions)