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

    let task = async {      //timer operates asynchronously 
        timer.Start()
        do! Async.Sleep 5000
        timer.Stop()
        }

    (task, observable)


let basicTimer1, timerEventStream = createTimerAndObservable 1000

timerEventStream |> Observable.subscribe (fun _ -> printfn "tick %A" DateTime.Now)
|> ignore

Async.RunSynchronously basicTimer1  //main program runs synchronousely 
