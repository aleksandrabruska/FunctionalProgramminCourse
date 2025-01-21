module Age

(*Example of using type abbreviations
with validation logic*)

type Age = private Age of int   

module Age =
    let create age =
        if age >= 0 && age <= 120 then
            Some (Age age) 
        else
            None          

    let value (Age age) = age 

// Usage
let validAge = Age.create 30 // Some (Age 30)
let invalidAge = Age.create 150 // None

match validAge with
| Some age -> printfn "Valid age: %d" (Age.value age)
| None -> printfn "Invalid age"

