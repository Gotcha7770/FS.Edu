module ReduceZeros

let reduceZeros items = 
    match items with
        | 0::tail -> [0]
        | _ -> items

let onlyZerosOrNaturals x y =
    match x, y with
        | 0, 0 -> true
        | 0, _ | _, 0 -> false        
        | _ -> true