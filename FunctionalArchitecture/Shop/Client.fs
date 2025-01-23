module Client
type LoyalityLevel = 
    | Regular
    | Silver
    | Gold

type Customer = {
    id: string
    loyalityLevel: LoyalityLevel
    validAccount: bool
}