module FS.Edu.FoldTests

open Swensen.Unquote
open Xunit

let average(list: double list): double =
    List.fold (+) 0.0 list / List.fold (fun acc _ -> acc + 1.0) 0.0 list
    
[<Fact>]
let ``average test``() = test <@ average [1; 2; 3; 4; 5] = 3.0 @>

let last(list: 'a list): 'a =
    List.fold (fun _ cur -> cur) (List.head list) list
    
[<Fact>]
let ``last test``() = test <@ last [1; 2; 3] = 3 @>

let reverse (list: 'a list) : 'a list =
    list |> List.fold (fun r c -> c :: r) []

[<Fact>]
let ``reverse test``() = test <@ reverse [1; 2; 3] = [3; 2 ;1] @>