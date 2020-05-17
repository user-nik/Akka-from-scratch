using Akka.Actor;
using Game.ActorModel.Actors;
using Game.ActorModel.ExternalSystems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.Web.Models
{
    public static class GameActorSystem
    {
        private static ActorSystem ActorSystem;
        private static IGameEventsPusher _gameEventsPusher;

        public static void Create()
        {
            _gameEventsPusher = new SignalRGameEventPusher();
            ActorSystem = ActorSystem.Create("GameSystem");

            ActorReference.GameController = ActorSystem.ActorOf<GameControllerActor>();


            ActorReference.SignalRBridge = ActorSystem.ActorOf(
                Props.Create(() => new SignalRBridgeActor(_gameEventsPusher, ActorReference.GameController)),
                "SignalRBridge"
                );


        }

        public static void ShutDown()
        {
            ActorSystem.Terminate().Wait();
        }

        public static class ActorReference
        {
            public static IActorRef GameController { get; set; }
            public static IActorRef SignalRBridge { get; set; }
        }
    }
}