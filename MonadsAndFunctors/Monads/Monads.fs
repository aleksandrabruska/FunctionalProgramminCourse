module Option = 
    //Option.bind
    //val it: (('a -> 'b option) -> 'a option -> 'b option)


    (*Example implementation of bind function for Option*)
    let bind f = function
        | Some v -> f v 
        | None   -> None

    let (>>=) f opt = bind f opt

    let add10 x = Some (x+10)
    let triple x = Some (x * 3)
    let return' a = Some a

    let result1a = add10 >>= (Some 12)   //Some22
    let result2a = bind add10 None      //None
    printfn "%A, %A" result1a result2a

    (*The same as above but with a library function*)
    let result1b = Option.bind add10 (Some 12)   //Some 22
    let result2b = Option.bind add10 None        //None
    printfn "%A, %A" result1a result2a  //True, True

    (*Checking monadic laws on example*)
    (*Left identity*)
    let leftIdCheck1 = ((bind add10 (Some 12)) = (add10 12))
    printfn "%A" leftIdCheck1

    (*Righ identity*)
    let rightIdCheck1 = ((bind return' (Some 12)) = (Some 12))
    let rightIdCheck2 = ((bind return' (None)) = (None))
    printfn "%A, %A" rightIdCheck1 rightIdCheck2

    (*Associativity*)
    let associativityCheck1 = ((bind add10 (bind triple (Some 12))) = (bind (fun x -> bind add10 (triple x)) (Some 12)))
    let associativityCheck2 = ((bind add10 (bind triple (None))) = (bind (fun x -> bind add10 (triple x)) (None)))
    printfn "%A, %A" associativityCheck1 associativityCheck2


module Result = 
    let bind f result =
        match result with
        | Ok value -> f value
        | Error e -> Error e 
    
    let (>>=) f res = bind f res

    let validateParity = function
        | x when x%2 = 0 -> Ok x
        | x -> Error "No parity"

    let validatePositivity = function
        | x when x > 0 -> Ok x
        | x -> Error "Not positive"

    let ex1a = validatePositivity >>= (validateParity >>= (Ok 22))
    let ex1b = validatePositivity >>= (validateParity >>= (Ok 33))
    let ex1c = validatePositivity >>= (validateParity >>= (Ok -22))
    let ex1d = validatePositivity >>= (validateParity >>= (Ok -33))
    printfn "%A, %A, %A, %A" ex1a ex1b ex1c ex1d
