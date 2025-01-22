module Railway

open System.Text.Json
open System.Text.RegularExpressions


//binding
let bind f = function
        | Ok v -> f v 
        | Error v-> Error v

let (>>=) res f = bind f res

//switch composition
let (>=>) aFun bFun x =
 match aFun x with
 | Ok y    -> bFun y
 | Error e -> Error e


// Define a sample class
type Person = {
    Name: string    //Two words
    Age: int        //between 18 and 150
    Email: string   //should contain @ and domain
    Phone: string   //should contain 8 chars
}
//let pattern = @"^\d{3}-\d{2}-\d{4}$"
let validateName (person : Person)  = 
    let namePattern = @"^[A-Za-z]+ [A-Za-z]+$"
    match Regex.IsMatch(person.Name, namePattern) with 
        | true -> Ok person
        | fasle -> Error "Name not valid"
   
let validateAge (person: Person)  =
    match person.Age with
        | age when age < 150 && age >= 18 -> Ok person
        | age -> Error "Age not valid"

let validateEmail (person: Person) =
    let emailPattern = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$$"
    match Regex.IsMatch(person.Email, emailPattern) with 
        | true -> Ok person
        | fasle -> Error "Email not valid"


let validatePhone (person: Person) =
    let phonePattern  = @"^\d{8}$"
    match Regex.IsMatch(person.Phone, phonePattern) with 
        | true -> Ok person
        | false -> Error "Phone number not valid"


let formatEmail p = 
    {p with Email = p.Email.ToLower()}

let switch f x =
    f x |> Ok

let tryCatch f p =
  try
    f p 
  with
    | ex -> Error ex.Message

let tee f x =
    f x |> ignore
    x

let printResult p = printfn "Here %A" p

let validatePerson p =
    p 
    |> tryCatch validateName
    >>= validateAge 
    >>= tryCatch validateEmail 
    >>= tryCatch validatePhone
    >>= switch formatEmail
    >>= switch (tee printResult)

let validatePerson' =
    tryCatch validateName 
    >=> validateAge
    >=> tryCatch validateEmail
    >=> tryCatch validatePhone
    >=> switch formatEmail
    >=> switch (tee printResult)


let jsonString = """{ "Name": "John Novak", "Age": 30, "Email": "abCC@abc.dk", "Phone": "12312312" }"""

let errors = []

// Deserialize JSON to an object
let person = JsonSerializer.Deserialize<Person>(jsonString)

//printfn "Name: %s, Age: %d, Email %s, Phone %s" person.Name person.Age person.Email person.Phone

let res = validatePerson' person