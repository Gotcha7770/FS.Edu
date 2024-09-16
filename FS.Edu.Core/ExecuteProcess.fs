module FS.Edu.ExecuteProcess

open System.Collections.Generic
open System.Diagnostics

type ProcessResult =
    { exitCode: int
      stdout: List<string>
      stderr: List<string> }

let private executeFromInfo (procInfo: ProcessStartInfo) =
    let proc = Process.Start(procInfo)

    let output = List<string>()
    proc.OutputDataReceived.Add(fun x -> output.Add(x.Data))
    let error = List<string>()
    proc.ErrorDataReceived.Add(fun x -> error.Add(x.Data))
//    let error = []
//    proc.ErrorDataReceived.Add(fun args -> List.append output [args.Data] |> ignore)

    proc.BeginErrorReadLine()
    proc.BeginOutputReadLine()
    proc.WaitForExit()

    { exitCode = proc.ExitCode
      stdout = output
      stderr = error }

let executeProcess name (args: IEnumerable<string>) =
    let procInfo = ProcessStartInfo(name, args)
    procInfo.RedirectStandardOutput <- true
    procInfo.RedirectStandardError <- true
    executeFromInfo procInfo

let executeProcessSilently name (args: IEnumerable<string>) =
    let procInfo = ProcessStartInfo(name, args)
    procInfo.UseShellExecute <- false
    procInfo.RedirectStandardOutput <- true
    procInfo.RedirectStandardError <- true
    procInfo.CreateNoWindow <- true

    executeFromInfo procInfo