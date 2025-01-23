module Shop
open DataRepository
open Purchase
open Price
open System

let (>>=) s f = Option.bind f s
let return' a = Some a

let displayAmount v = printfn "%f" v

let getDiscount = 
    fun p -> (DataRepository.getCustomer(p.customerID))  //side effecs
    >> Option.bind Discount.calculate //no side effects
    >> Option.map (displayAmount)   //side effects

let getPrice = 
    fun p -> (DataRepository.getCustomer(p.customerID), p) //side effecs
    >> fun (c, p) -> 
        (c |> Option.bind 
        (fun customer -> return'( Price.calculate Discount.calculate p.amount customer))) //no side effects
    >> Option.map displayAmount   //side effects
