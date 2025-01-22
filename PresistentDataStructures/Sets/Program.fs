let set1 = set [2;4;10;20]
let set2 = Set.add 1 >> Set.add 3 >> Set.add 2 <| set1
printfn "%A" set2

let set3 = Set.remove 2 set2
printfn "%A" set3

let set4 = set [1; 2; 4; 22]
let set5 = Set.union set1 set4
printfn "%A" set5

let set6 = Set.intersect set1 set4
printfn "%A" set6

let set7 = Set.difference set1 set4
printfn "%A" set7

let set8 = Set.difference set4 set1
printfn "%A" set8

let set9 = Set.filter (fun x -> x%2 = 0) set4
printfn "%A" set9

let set10 = Set.map (fun x -> x*2) set4
printfn "%A" set10

let res1 = Set.fold (fun s v -> s - v) 0 set1
printfn "%A" res1

let res2 = Set.foldBack (fun s v -> s - v) set1 0
printfn "%A" res2