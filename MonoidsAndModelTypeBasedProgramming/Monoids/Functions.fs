module FunctionsTest
open FsCheck
open FsCheck.Xunit

(*Testing that function int->int and append is a modoid*)
[<Property>]
let ``test composition associativity`` (f1: (int -> int)) (f2: (int -> int)) (f3: (int -> int)) (x: int) = 
    ((f1 >> f2) >> f3) x = (f1 >> (f2 >> f3)) x

[<Property>]
let ``test composition identity element`` (f: (int -> int)) (x: int) = 
    //printfn "F: %d %d %d" (f 0) (f 1) (f 2)
    //printfn "Fid: %d %d %d" ((f >> id) 0) ((f >> id) 1) ((f >> id) 2)
    ((f >> id) x = f x) && ((id >> f) x = f x)
 
Check.Quick ``test composition associativity``
Check.Quick ``test composition identity element``

