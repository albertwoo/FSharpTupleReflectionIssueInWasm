#nowarn "0020"

open System
open Microsoft.Extensions.DependencyInjection
open Microsoft.AspNetCore.Components.WebAssembly.Hosting
open Fun.Blazor

type TupleWith6Items = int * int * int * int * int * int

let app =
    let msgForTupleWith5Items =
        try
            Reflection.FSharpValue.MakeTuple([| for i in 1..5 -> i |], typeof<int * int * int * int * int>) |> sprintf "%A"
        with ex ->
            sprintf "%A" ex

    let msgForTupleWith6Items =
        try
            Reflection.FSharpValue.MakeTuple([| for i in 1..6 -> i |], typeof<int * int * int * int * int * int>) |> sprintf "%A"
        with ex ->
            sprintf "%A" ex

    let msgForTupleWith7Items =
        try
            Reflection.FSharpValue.MakeTuple([| for i in 1..7 -> i |], typeof<int * int * int * int * int * int * int>)
            |> sprintf "%A"
        with ex ->
            sprintf "%A" ex

    div {
        style { fontSize 30 }
        div { nameof msgForTupleWith5Items + ": " + msgForTupleWith5Items }
        div { nameof msgForTupleWith6Items + ": " + msgForTupleWith6Items }
        div { nameof msgForTupleWith7Items + ": " + msgForTupleWith7Items }
    }

let builder = WebAssemblyHostBuilder.CreateDefault(Environment.GetCommandLineArgs())

builder.AddFunBlazor("#app", app)

builder.Services.AddFunBlazorWasm()

builder.Build().RunAsync()
