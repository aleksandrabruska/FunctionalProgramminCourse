open System
open System.Threading

(*Timer
Events: a second passed
Handler: printing the time
*)

let createTimerAndObservable timerInterval =
    let timer = new System.Timers.Timer(float timerInterval)
    timer.AutoReset <- true

    //observable is a stream of events
    let observable = timer.Elapsed

    let task = async {
        timer.Start()
        do! Async.Sleep 5000
        timer.Stop()
        }

    (task, observable)


let basicTimer1, timerEventStream = createTimerAndObservable 1000

timerEventStream 
    |> Observable.scan (fun count _ -> count + 1) 0     //scan accumulates state for each event
    |> Observable.subscribe (fun count -> printfn "tick %A with count %d" DateTime.Now count)
    |> ignore

Async.RunSynchronously basicTimer1