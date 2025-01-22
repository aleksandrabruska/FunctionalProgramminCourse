module Multiset
type Multiset<'a when 'a: comparison> = Map<'a, int>

let rec inv (ms: Multiset<'a>) = ms |> Map.forall (fun n s -> s >= 0)

let insert (key: 'a) (value: int) (ms: Multiset<'a>) = 
    let addToVal y x = 
        match x with
        | Some s -> Some (s + y)
        | None -> None
    let innerInsert = function
        | (k, v) when ms.ContainsKey(k) -> Map.change k (addToVal v) ms
        | (k,v) -> (Map.add k v ms)
    let result = innerInsert (key, value) 
    if inv result then result else ms 


let union (ms1: Multiset<'a>) (ms2: Multiset<'a>) = 
    let rec adding  = function
        | (msx,msx2) -> 
            let addToMap  = function    
                | ms when List.isEmpty ms -> Map.toList msx
                | (key, value)::[] -> (Map.toList (insert key value msx))
                | (key, value)::ms -> adding ((insert key value msx), Map.ofList ms)
            addToMap (Map.toList msx2)


    Map.ofList (adding (ms1, ms2))
    
    //Map.ofList(addToMap (Map.toList ms2))