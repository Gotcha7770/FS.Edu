module FS.Edu.ExecuteProcess

open System.Diagnostics
open System.Text

type ProcessResult =
    { exitCode: int
      stdout: string
      stderr: string }

let private executeFromInfo (procInfo: ProcessStartInfo) =
    let proc = Process.Start(procInfo)

    let output = StringBuilder()
    proc.OutputDataReceived.Add(fun args -> output.Append(args.Data) |> ignore)
    let error = StringBuilder()
    proc.ErrorDataReceived.Add(fun args -> error.Append(args.Data) |> ignore)

    proc.BeginErrorReadLine()
    proc.BeginOutputReadLine()
    proc.WaitForExit()

    { exitCode = proc.ExitCode
      stdout = output.ToString()
      stderr = error.ToString() }

let executeProcess name args =
    let procInfo = ProcessStartInfo(name, args)
    procInfo.RedirectStandardOutput <- true
    procInfo.RedirectStandardError <- true
    executeFromInfo procInfo

let executeProcessSilently name args =
    let procInfo = ProcessStartInfo(name, args)
    procInfo.UseShellExecute <- false
    procInfo.RedirectStandardOutput <- true
    procInfo.RedirectStandardError <- true
    procInfo.CreateNoWindow <- true

    executeFromInfo procInfo