(*Pow function*)
(*Not tail recursive*)
let rec pow = function
    | (s: string, 1) -> s
    | (s: string, n) -> s + pow (s, n-1)

(*Tail recursive using accumulatibe value*)
let pow' (input: string * int) =
    let rec powInner acc = function
        | (s: string, 1) -> acc+s
        | (s: string, n) -> powInner (acc + s) (s, n-1)
    powInner "" input

let res1 = pow ("abc", 12)
printfn "%A" res1

(*Factorial example*)
(*Not tail recursive*)
let rec fact' = function
    | 0L | 1L -> 1L
    | n -> n * fact' (n-1L)

(*Tail recursive using accumulative value*)
let fact'' n =
    let rec factInner = function
        | (0L, m) -> m
        | (n, m) -> factInner (n-1L, n*m)
    factInner (n, 1)

let res2a = fact'' 5
printfn "%A" res2a

(*Tail recursive using continuations*)
let rec tr checker cont last value =
    if checker value
    then tr checker cont last (cont value)
    else last value

// val tr : checker:('input -> bool) ->
//          cont:('input -> 'input) ->
//          last:('input -> 'output) ->
//          value:'a
let fact''' = tr (fun (n,_) -> n<>0L)       // checker
               (fun (n,m) -> (n-1L, m*n)) // cont
               (fun (_,m) -> m)           // last

let res2b = fact''' (5, 1)
printfn "%A" res2b


(*Populating list in opposite direction using continuations*)
let rec bigListC c = function
    | 0 -> (printfn "last"; c [])
    | n -> bigListC (fun res -> printfn "execute %d" (n-1)
                                printfn "res %A" res
                                c ((n-1)::res)
                    ) (n-1)

let res3 = bigListC (fun a ->  a) 3
printfn "%A" res3
