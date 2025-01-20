(*Example implementation of apply function for Option*)
module Option = 
    let apply optFunc optValue =
        match optFunc, optValue with
        | Some f, Some x -> f x |> Some
        | _ -> None

    let pure x = Some x
    let compose f g x = f (g x)

    //val apply: optFunc: ('a -> 'b) option -> optValue: 'a option -> 'b option

    let (<*>) optF optX = apply optF optX


    let add10Opt = Some ((+) 10)
    let tripleOpt = Some ((*) 3) 
    let addOpt = Some (+)
    let add10 x = x + 10

    //let result1a = addOpt <*> (Some 12) <*> (Some 3) //Some22
    let result1a = add10Opt <*> (Some 12)//Some22
    let result2a = apply add10Opt None      //None

    printfn "%A, %A" result1a result2a

    (*Checking applicative laws on example*)

    (*Identity*)
    let identityCheck1 = (pure id <*> (Some 22)) = Some 22
    let identityCheck2 = (pure id <*> (None)) = None
    printfn "%A, %A" identityCheck1 identityCheck2

    (*Composition*)
    let compositionCheck1 = (pure compose <*> add10Opt <*> tripleOpt <*> Some 22) = (add10Opt <*> (tripleOpt <*> Some 22))
    let compositionCheck2 = (pure compose <*> add10Opt <*> tripleOpt <*> None) = (add10Opt <*> (tripleOpt <*> None))
    printfn "%A, %A" compositionCheck1 compositionCheck2

    (*Homomorphism*)
    let homomorphismCheck1 = (pure add10 <*> pure 22) = (pure (add10 22)) 
    printfn "%A" homomorphismCheck1

    (*Interchange*)
    let interchangableCheck1 = (add10Opt <*> pure 22) = (pure (fun a -> a 22) <*> add10Opt)
    printfn "%A" interchangableCheck1

    (*Examples*)
    (*1.Multiple wrapped arguments*)
    let ex1a = addOpt <*> Some 10 <*> Some 22   //Some 32
    let ex1b = addOpt <*> Some 10 <*> None      //None
    printfn "%A, %A" ex1a ex1b 

module List = 
    (*2.List of functions*)
    let apply funcList valueList =
        [ for f in funcList do
            for x in valueList -> f x ]
    let (<*>) funcList valueList = apply funcList valueList

    let ex2 = [(*)2; (+)5] <*> [1;2;3]
    printfn "%A" ex2