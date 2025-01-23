module DataRepository
open Client

let customers = [{id = "123456"; loyalityLevel = LoyalityLevel.Silver; validAccount = true};
        {id = "231413"; loyalityLevel = LoyalityLevel.Gold; validAccount = true};
        {id = "341231"; loyalityLevel = LoyalityLevel.Regular; validAccount = true};
        {id = "324234"; loyalityLevel = LoyalityLevel.Regular; validAccount = false};
        {id = "12212"; loyalityLevel = LoyalityLevel.Silver; validAccount = false}]

let getCustomer id =
    List.tryFind (fun c -> c.id = id) customers
 
