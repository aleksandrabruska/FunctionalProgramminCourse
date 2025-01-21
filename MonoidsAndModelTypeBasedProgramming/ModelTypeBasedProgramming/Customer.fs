module Customer
open System
(*Custom Equality*)
[<CustomEquality; CustomComparison>]
type Customer =
    { CustomerId : int; Name : string; Age : int; Town : string }

    interface IEquatable<Customer> with                                     //comparing with another customer
        member this.Equals other = other.CustomerId.Equals this.CustomerId  

    override this.Equals other =                                            //comparing with any object
        match other with
        | :? Customer as p -> (this :> IEquatable<_>).Equals p              //delegation
        | _ -> false

    override this.GetHashCode () = this.CustomerId.GetHashCode()

    interface IComparable with
        member this.CompareTo other =
            match other with
            | :? Customer as p -> (this :> IComparable<_>).CompareTo p     //?: - type testing and casting
            | _ -> -1

    interface IComparable<Customer> with
        member this.CompareTo other = other.CustomerId.CompareTo this.CustomerId
