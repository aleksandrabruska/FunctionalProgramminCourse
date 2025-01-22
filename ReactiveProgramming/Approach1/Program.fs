open System
open System.Threading

(*Timer
Events: a second passed
Handler: printing the time
*)

let createTimer timerInterval eventHandler =
    let timer = new System.Timers.Timer(float timerInterval)
    timer.AutoReset <- true
    timer.Elapsed.Add eventHandler

    async {
        timer.Start()
        do! Async.Sleep 5000
        timer.Stop()
        }

let basicHandler _ = printfn "tick %A" DateTime.Now

let basicTimer1 = createTimer 1000 basicHandler

Async.RunSynchronously basicTimer1