module Discount
open Purchase
open Client

let calculate customer = 
    if (customer.validAccount) then
        match customer.loyalityLevel with
        | LoyalityLevel.Silver -> Some 10.0
        | LoyalityLevel.Gold -> Some 20.0
        | _ -> None
    else
        None


