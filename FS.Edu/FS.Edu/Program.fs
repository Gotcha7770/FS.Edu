// Learn more about F# at http://fsharp.org

open System

// Выводит таблицу истинности для заданной функции
let printTruthTable f =
    printfn "      |  true | false |"
    printfn "      +-------+-------+"
    printfn " true |  %5b  |  %5b  |" (f true true) (f true false)
    printfn " false|  %5b  |  %5b  |" (f false true) (f false false)
    printfn "      +-------+-------+"
    printfn "\n"

[<EntryPoint>]
let main argv =
    printTruthTable (&&)
    printTruthTable (||)
    0 // return an integer exit code
