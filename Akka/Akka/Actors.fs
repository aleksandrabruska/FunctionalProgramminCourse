module Actors

open Akka
open Akka.FSharp
open Akka.Actor
open System
open Messages

let personActor (name: string) (msg: PersonMessage)=
    match msg with
    | LoginSuccess -> printfn "I ve logged in ~%s" name
    | NewUpdate txt -> printfn "I've got an update message!"


let usersActor (mailbox: Actor<UsersMessage>) =
    let anne = spawn mailbox.Context "anne" (actorOf (personActor "ANNE"))
    let paul = spawn mailbox.Context "paul" (actorOf (personActor "PAUL"))
    let notifications = select "/user/notifications" mailbox.Context.System
    
    let rec loop() = actor {
        let! message = mailbox.Receive()
        printfn "Enter your login (anne or paul): "
        match message with 
        | Start -> 
            let txt = Console.ReadLine()
            if (txt = "Exit") then
                mailbox.Context.System.Terminate() |> ignore
            else if (txt.Equals("anne") || txt.Equals("paul")) then
                notifications <! (InputSuccess (Username txt))

            else 
                 notifications <! (InputError "Error")
        | _ -> mailbox.Unhandled message
        // handle an incoming message
        return! loop()
        
    }
    loop()
    
    
let smsActor (mailbox: Actor<SmsMailMessage>) (msg : SmsMailMessage) = 
    let user = 
        match msg with 
        | Username un -> 
            select ("/user/users/" + un) mailbox.Context.System
        | Update up ->
            select ("/user/users/*") mailbox.Context.System

    printfn "I am sms"
    user <! LoginSuccess

let emailActor (mailbox: Actor<SmsMailMessage>) (msg: SmsMailMessage) = 
    match msg with
    | Username un ->
        let user = select ("user/users/" + un) mailbox.Context.System
        user <! LoginSuccess
    | Update up ->
        let user = select ("/user/users/*") mailbox.Context.System
        user <! NewUpdate up
    
    printfn "I am email"


let notificationSupervisorActor (mailbox: Actor<Input>) = 
    let sms = spawn mailbox.Context "sms" (actorOf2 smsActor)
    let email = spawn mailbox.Context "email" (actorOf2 emailActor)

    let rec loop () = actor {
        let! message = mailbox.Receive()
        match message with 
        | InputSuccess username -> email (*sms*) <! username
        | InputError txt -> mailbox.Unhandled message
        | SendUpdates txt -> email <! Update txt
        printfn "I am supervisor"
        return! loop()
    }
    loop()

    