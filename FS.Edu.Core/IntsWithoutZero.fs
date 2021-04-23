module IntsWithoutZero

    // make the type constructor private
    type T = private IntsWithoutZero of int

    // factory that can apply the constraints
    let create value =
        // do check for zero here
        match value with
            | 0 -> None
            | _ -> IntsWithoutZero(value) |> Some

    // a method to extract the private data
    let value (IntsWithoutZero v) = v

    //пример как матчить int исключая 0 без танцев
    let myDividedValue data =
        match data with
            | 0 -> None
            | _ -> Some(10 / data)