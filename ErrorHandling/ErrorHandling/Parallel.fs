module Parallel

open System.Text.Json
open System.Text.RegularExpressions


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


let plus combineOks combineErrors switch1 switch2 x = 
    match (switch1 x),(switch2 x) with
    | Ok s1, Ok s2       -> Ok (combineOks s1 s2)
    | Error f1, Ok _     -> Error f1
    | Ok _ , Error f2    -> Error f2
    | Error f1, Error f2 -> Error (combineErrors f1 f2)


let (&&&) v1 v2 =
    let addOk r1 r2 = r1
    let addError s1 s2 = s1 + "; " + s2 
    plus addOk addError v1 v2

let parallelValidation = 
    validateName
    &&& validateAge
    &&& validateEmail
    &&& validatePhone

let jsonString = """{ "Name": "John Novak", "Age": 330, "Email": "abCC@abc.dk", "Phone": "1231232" }"""

let errors = []

// Deserialize JSON to an object
let person = JsonSerializer.Deserialize<Person>(jsonString)

//printfn "Name: %s, Age: %d, Email %s, Phone %s" person.Name person.Age person.Email person.Phone
let result = parallelValidation person
printfn "%A" result