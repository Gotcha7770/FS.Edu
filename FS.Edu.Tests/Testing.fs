module Testing

open Expecto
open FsUnit

let testCases name f cases =
    testList
        name
        [ for caseName, input, expected in cases do
              test caseName {
                  let result = f input

                  result |> should equal expected
              }
        ]