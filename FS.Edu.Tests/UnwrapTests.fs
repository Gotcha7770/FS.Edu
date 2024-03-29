﻿module FS.Edu.Tests.UnwrapTests

open Swensen.Unquote
open Xunit

let computation: Async<Result<int option, string>> =
        let option = Some 42
        let result = Ok option
        async.Return result
    
[<Fact>]
let ``Unwrap it in procedural style`` =
    let async = computation
    let result = async |> Async.RunSynchronously
    let option =
        match result with
        | Ok x -> x
        | Error e -> Some -1
    let value =
        match option with
        | Some x -> x
        | None -> 0
    test <@ value = 42 @>
    
[<Fact>]
let ``Unwrap with ce`` =
    let ce =  async {
        let! result = computation
        return result |> Result.map      
    }
    
    let value = ce |> Async.RunSynchronously
    test <@ value = 42 @>