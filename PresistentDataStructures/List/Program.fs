let list = [3;4;5]
let list2 = 1::list
let list3 = [1;2]@list
printfn "%A" list2
printfn "%A" list3

let max = List.fold (fun acc x -> if acc > x then acc else x) 0 [2;3;7;3;1]
printfn "%A" max

let cube = List.map (fun x -> x * x * x) [2;3;7;3;1]
printfn "%A" cube

let functions = [(fun x -> x+4); (fun x -> x*2); (fun x -> x*x)]
let res = List.map (fun f -> f(1)) functions
printfn "%A" res

let zipped = List.zip [1;2;3;4;5] ["a"; "b"; "c"; "d"; "e"]
printfn "%A" zipped