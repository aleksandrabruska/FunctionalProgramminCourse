//Option.map
//val it: (('a -> 'b) -> 'a option -> 'b option)

module Option = 
    (*Example implementation of map function for Option*)
    let map f opt =
        match opt with
        | Some value -> Some (f value)
        | None -> None

    let (<!>) f opt = map f opt

    let add10 x = x + 10
    let triple x = x * 3

    let result1a = add10 <!> (Some 12)   //Some22
    let result2a = map add10 None      //None
    printfn "%A, %A" result1a result2a


    (*Checking functor laws on example*)
    (*Identity*)
    let identityCheck1 = (map id (Some 12)) = (Some 12)
    let identityCheck2 = (map id None) = (None)
    printfn "%A, %A" identityCheck1 identityCheck2  //True, True

    (*Composition*)
    let compositionCheck1 = (((Some 22) |> map add10 |> map triple) 
                                = ((Some 22) |> map (add10 >> triple))) 
    let compositionCheck2 = (((None) |> map add10 |> map triple) 
                                = ((None) |> map (add10 >> triple))) //f>>g = g(f(x))

    printfn "%A, %A" compositionCheck1 compositionCheck2

module List = 
    (*Another example with a List*)
    let resultList = [1.0 .. 10.0] |> List.map (fun x -> x**2)
    printfn "%A" resultList
