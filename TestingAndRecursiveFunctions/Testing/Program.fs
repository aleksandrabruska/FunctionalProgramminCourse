open FsCheck
open FsCheck.Xunit
 

exception WrongArgument

(*Example with list reversing*)
[<Property>]
let ``Reverse of reverse of a list is the original list ``
    (xs:list<int>) =
  List.rev(List.rev xs) = xs

Check.Quick ``Reverse of reverse of a list is the original list ``

(*Testing the factorial properties*)
let rec factorial = function
    | 0 -> 1
    | n when n < 0 -> raise WrongArgument
    | n -> n * factorial (n-1)


[<Property>]
let ``Simple factorial test`` (n: int) =
    factorial(n+1)/(n+1) = factorial(n)

(*
let generateNonNegativeInteger xs = 
  gen {
        let! i = Gen.choose (0, 10) 
        return  i
      }
      *)

[<Property>]
let ``first property check`` =
    Arb.generate<int32>
    |> Gen.filter (fun i -> i >= 0 && i < 12)    
    |> Arb.fromGen
    |> Prop.forAll <| fun i ->
        factorial(i+1)/(i+1) = factorial(i)


let ``exception when negative number`` = 
    Arb.generate<int32>
    |> Gen.filter (fun i -> i < 12)    
    |> Arb.fromGen
    |> Prop.forAll <| fun i ->
    (i < 0) ==> Prop.throws<WrongArgument,_> 
            (lazy (factorial i))    //to avoid evaluating eagerly


Check.Quick ``first property check`` 
Check.Quick ``exception when negative number``
Check.One({ Config.Quick with MaxTest = 1 }, fun () -> factorial 0 = 1)



(*Testing that List and append is a modoid*)
[<Property>]
let ``test append associativity`` (x1: List<int>) (x2: List<int>) (x3: List<int>) = 
    List.append (List.append x1 x2) x3 = List.append x1 (List.append x2 x3) 

[<Property>]
let ``test append identity element`` (xs: List<int>) = 
    List.append xs [] = xs
 
Check.Quick ``test append associativity``
Check.Quick ``test append identity element``

let moreLazy a = a <> 0 ==> (lazy (1/a = 1/a))







