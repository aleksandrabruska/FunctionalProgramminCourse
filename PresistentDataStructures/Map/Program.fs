let products = Map.ofList [("cheese", 32); ("ham", 28); ("milk", 12)]
let products2 = Map.add "bread" 10 products
printfn "%A" products2

let receipt = Map.fold (fun (s1, s2) k v -> s1 + k + "\n", s2 + v) ("", 0) products2
let receipt2 = Map.foldBack (fun k v (s1, s2) -> s1 + k + "\n", s2 + v) products2 ("", 0)
printfn "%A" receipt
printfn "%A" receipt2

let f = Map.find "cheese" products
printfn "%A" f

let tf = Map.tryFind "table" products
printfn "%A" tf

let c = Map.containsKey "ham" products
printfn "%A" c

let e = products2 |> Map.exists (fun k v -> k.Length = v)
printfn "%A" e
