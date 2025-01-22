module Messages

type SmsMailMessage =
    | Username of string
    | Update of string

type Input =
    | InputSuccess of SmsMailMessage
    | InputError of string
    | SendUpdates of string

type PersonMessage =
    | LoginSuccess
    | NewUpdate of string

type UsersMessage = 
    | Start