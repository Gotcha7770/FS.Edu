module FS.Edu.Tests.UnwrapTests

open Swensen.Unquote
open Xunit

let computation: Async<Result<int option, string>> =
        let option = Some 2
        let result = Ok option
        async.Return result
    
[<Fact>]
let ``Unwrap it in procedural style`` =
    let result = computation |> Async.RunSynchronously
    let option =
        match result with
        | Ok x -> x
        | _ -> Some -1
    let value =
        match option with
        | Some x -> x
        | None -> 0

    test <@ value = 42 @>

[<Fact>]
let ``Unwrap Async<Option<a`>>`` =
    let unwrapOption asyncValue =  async {
        match! asyncValue with
        | Some value -> return value
        | None -> return  0
    }
    
    let value =
        Some 42
        |> async.Return
        |> unwrapOption
        |> Async.RunSynchronously

    test <@  value = 42 @>

[<Fact>]
let ``Unwrap with ce`` = 
    let ce = async {
        let! option = async {
            match! computation with
            | Ok option -> return option
            | _ -> return  Some -1
        }
        return match option with
                | Some x -> x
                | None -> 0
    }
    
    let value = ce |> Async.RunSynchronously

    test <@ value = 42 @>