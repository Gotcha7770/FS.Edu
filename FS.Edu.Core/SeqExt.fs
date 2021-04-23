module FS.Edu.SeqExt

let repeat value times = seq { for _ in 1 .. times -> value }