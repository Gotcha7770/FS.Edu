module FS.Edu.Tests.ProcessExecutionTests

open System
open FS.Edu.ExecuteProcess
open NUnit.Framework

[<Test>]
let ``Execute cmd dir`` () = 
    let result = executeProcessSilently "cmd" "/c dir"
    
    Assert.AreEqual(0, result.exitCode)
    Assert.IsNotEmpty(result.stdout)
    
[<Test>]
let ``Execute git branch -vv`` () =
    Environment.CurrentDirectory <- "C:\Users\Gotcha\Dev\GST"
    let result = executeProcessSilently "git" "branch -vv"
    
    Assert.AreEqual(0, result.exitCode)
    Assert.IsNotEmpty(result.stdout)    
    
