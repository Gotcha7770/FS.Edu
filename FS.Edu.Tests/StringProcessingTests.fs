module FS.Edu.StringProcessingTests

open System
open System.Linq
open NUnit.Framework
open SeqExt


//[<SetUp>]
//let Setup () = ()

[<Test>]
let ``Fold test`` () = 
    let result =
        {1..5}
        |> Seq.map (fun n -> "Test\r\n")
        |> Seq.fold (fun agg cur -> agg + cur) String.Empty
    let expected = "Test\r\nTest\r\nTest\r\nTest\r\nTest\r\n"
    
    Assert.AreEqual(expected, result)

[<Test>]  
let ``Split test`` () =
    let source = "Test\r\nTest\r\nTest\r\nTest\r\nTest\r\n"
    let result = source.Split("\r\n", StringSplitOptions.RemoveEmptyEntries)
    let expected = Enumerable.Repeat("Test", 5)
    
    //Assert.AreEqual(expected, result)
    CollectionAssert.AreEqual(expected, result)