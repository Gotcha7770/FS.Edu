module FS.Edu.Tests.UnwrapTests

open Expecto
open FsUnit

let computation: Async<Result<Option<int>, string>> =
        let option = Some 42
        let result = Ok option
        async.Return result
    
[<Tests>]
let tests =
    testList "Unwrap tests" [
        test "procedural style" {
            let result = computation |> Async.RunSynchronously
            let option =
                match result with
                | Ok x -> x
                | _ -> Some -1
            let value =
                match option with
                | Some x -> x
                | None -> 0

            value |> should equal 42
        }
        
        test "computational expressions" {
            let expression = async {
                 let! option = async {
                     match! computation with
                     | Ok option -> return option
                     | _ -> return  Some -1
                 }
                 
                 return
                    match option with
                    | Some x -> x
                    | None -> 0
            }
            
            let result = expression |> Async.RunSynchronously
            
            result |> should equal 42
        }
    ]