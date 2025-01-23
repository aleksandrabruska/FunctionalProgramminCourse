module Price
let calculate calculateDiscount value customer = 
    match calculateDiscount customer with
    | Some d -> value * (1.0 - (d/100.0)) 
    | None -> value
    