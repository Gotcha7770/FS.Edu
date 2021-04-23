// Learn more about F# at http://fsharp.org
namespace Main

open System

module Equality =
    //свой оператор не равно
    let (!=) x y = x <> y

module TruthTables =

    open Equality

    //форма записи для таблицы истинности
    let andTruthTable x y =
        match x, y with
        | true, true-> true
        | _, _ -> false

    let orTruthTable x y =
        match x, y with
        | false, false -> false
        | _, _ -> true

    let xorTruthTable x y =
        match x, y with
        | x, y when x != y -> true
        | _, _ -> false

    // Выводит таблицу истинности для заданной функции
    let printTruthTable f =
        printfn "      |  true | false |"
        printfn "      +-------+-------+"
        printfn " true |  %5b  |  %5b  |" (f true true) (f true false)
        printfn " false|  %5b  |  %5b  |" (f false true) (f false false)
        printfn "      +-------+-------+"
        printfn "\n"

module BinaryTree =
    type BinaryTree =
        | Node of int * BinaryTree * BinaryTree
        | Empty

module Main =

    open TruthTables

    let main argv =
        printTruthTable (&&)
        printTruthTable (||)
        0 // return an integer exit code
