module FS.Edu.Tests.FoldTests

open Expecto
open FS.Edu.Tests.Testing

let average(list: float list): float =
    match list with
    | [] -> 0.0
    | _ -> ((0.0, list) ||> List.fold (+))
           / ((0.0, list) ||> List.fold (fun acc _ -> acc + 1.0))

let last(list: 'a list): 'a option =
    match list with
    | [] -> None
    | _ -> Some ((List.head list, list) ||> List.fold (fun _ cur -> cur))

let reverse (list: 'a list) : 'a list =
    ([], list) ||> List.fold (fun r c -> c :: r)



let averageCases =
    [
        "empty", [], 0.0
        "one", [1.0], 1.0
        "many", [1;2;3], 2.0
    ]
    
let lastCases =
    [
        "empty", [], None
        "one", [1], Some 1
        "many", [1;2;3], Some 3
    ]
    
let reverseCases =
    [
        "empty", [], []
        "one", [1], [1]
        "many", [1;2;3], [3;2;1]
    ]

[<Tests>]
let tests =
    testList "List functions" [
        testCases "average" average averageCases
        testCases "last" last lastCases
        testCases "reverse" reverse reverseCases
    ]