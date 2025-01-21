module Contact

type EmailAddr = string //abbrev example

//record example
type NameInfo = {
  FirstName: string
  MiddleName: string option
  LastName: string
}
type EmailInfo = {
  EmailAddress: EmailAddr
  IsEmailVerified: bool
}
type PostalInfo ={
    street: string
    number: int
    city: string
}

type ContactInfo =                                  //sum example
  | OnlyEmail of EmailInfo
  | OnlyPostal of PostalInfo
  | BothEmailAndPostal of EmailInfo * PostalInfo    //tuple example
 
type Contact = {
  name: NameInfo
  contact: ContactInfo
}