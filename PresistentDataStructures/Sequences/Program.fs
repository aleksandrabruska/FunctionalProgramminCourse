let seqFinite = seq [1;2;4;8]
let seqInfinite = Seq.initInfinite (fun i -> 2.0**i)

let item = Seq.item 3 seqInfinite   //only 3rd element evaluated
printfn "%f" item

let item2 = Seq.item 3 seqInfinite  //3rd element evaluated again
printfn "%f" item

let cachedSeq = Seq.cache seqInfinite
let item3 = Seq.item 4 cachedSeq  //elements 0-4 evaluated
printfn "%f" item3

let item4 = Seq.item 2 cachedSeq  //no evaluation needed
printfn "%f" item4