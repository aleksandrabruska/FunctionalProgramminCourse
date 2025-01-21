module ListTest
open FsCheck
open FsCheck.Xunit

(*Testing that List and append is a modoid*)
[<Property>]
let ``test append associativity`` (x1: List<int>) (x2: List<int>) (x3: List<int>) = 
    List.append (List.append x1 x2) x3 = List.append x1 (List.append x2 x3) 

[<Property>]
let ``test append identity element`` (xs: List<int>) = 
    List.append xs [] = xs
 
Check.Quick ``test append associativity``
Check.Quick ``test append identity element``

