module FS.Edu.Tests.ProcessExecutionTests

open FS.Edu.ExecuteProcess
open NUnit.Framework

[<Test>]
let ``Execute cmd dir`` () = 
    let result = executeProcessSilently "cmd" "/c dir"
    
    Assert.AreEqual(0, result.exitCode)
    Assert.IsNotEmpty(result.stdout)
