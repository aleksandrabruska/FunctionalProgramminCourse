// For more information see https://aka.ms/fsharp-console-apps
open Akka
open Akka.FSharp
open Akka.Actor
open Actors
open System
open Messages

let actorSystem = System.create "ActorSystem" (Configuration.load())

let users = spawnOpt actorSystem "users"
              usersActor [SpawnOption.SupervisorStrategy(Strategy.OneForOne( 
                (fun error -> match error with
                               | :? ArithmeticException -> Directive.Resume
                               | _ -> Directive.Restart    ))) ]

let notificationSupervisor = spawnOpt actorSystem "notifications" 
                                notificationSupervisorActor [SpawnOption.SupervisorStrategy(Strategy.OneForOne( 
                (fun error -> match error with
                               | :? ArithmeticException -> Directive.Resume
                               | _ -> Directive.Restart    ))) ]

users <! Start

(*
notificationSupervisor <! SendUpdates "New version of application is available!"
notificationSupervisor <! SendUpdates "New version of application is available!"
notificationSupervisor <! SendUpdates "New version of application is available!"
notificationSupervisor <! SendUpdates "New version of application is available!"
notificationSupervisor <! SendUpdates "New version of application is available!"
notificationSupervisor <! SendUpdates "New version of application is available!"
*)
actorSystem.WhenTerminated.Wait()