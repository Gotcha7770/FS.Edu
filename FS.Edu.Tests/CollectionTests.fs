module FS.Edu.Tests.CollectionTests

open System
open System.Linq
open NUnit.Framework

[<Test>]
let ``Different collection types initialization`` () = 
    let seq = Seq.empty
    let list1 = List.empty
    let list2 = []
    let array = Array.empty
    
    Assert.Pass