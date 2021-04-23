module FS.Edu.StringProcessingTests

open System
open System.Linq
open System.Text.RegularExpressions
open NUnit.Framework

[<Test>]
let ``Fold test`` () = 
    let result =
        {1..5}
        |> Seq.map (fun _ -> "Test\r\n")
        |> Seq.fold (fun agg cur -> agg + cur) String.Empty
    let expected = "Test\r\nTest\r\nTest\r\nTest\r\nTest\r\n"
    
    Assert.AreEqual(expected, result)

[<Test>]  
let ``Split test`` () =
    let source = "Test\r\nTest\r\nTest\r\nTest\r\nTest\r\n"
    let result = source.Split("\r\n", StringSplitOptions.RemoveEmptyEntries)
    let expected = Enumerable.Repeat("Test", 5)
    
    CollectionAssert.AreEqual(expected, result)
    
[<Test>]
let ``RegEx test`` () =
    let source = "Branch { Id: 1, IsGone: True}"
    let r1 = Regex("IsGone: True")
    let m1 = r1.Match source
    
    Assert.IsTrue(m1.Success)
    
    let r2 = Regex("IsGone: False")
    let m2 = r2.Match source
    
    Assert.IsFalse(m2.Success)