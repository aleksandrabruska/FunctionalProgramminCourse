open Multiset
let ms: Multiset<string> = Map.ofList([("a", 1); ("b", 2); ("c", 3)])

printfn "%b" (inv ms)

let ms11 = (insert "b" 5 ms)
printfn "%A" ms11

let ms22 = (insert "aa" 9 ms11)
printfn "%A" ms22


let ms2: Multiset<string> = Map.ofList([("h", 13); ("0", 1); ("a", 6); ("c",8)])

let empty: Multiset<string> = Map.ofList([])
let union = union empty ms2
printfn "%A" union