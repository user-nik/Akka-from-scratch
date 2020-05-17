using Akka.Actor;
using Game.ActorModel.ExternalSystems;
using Game.ActorModel.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.ActorModel.Actors
{
    public class SignalRBridgeActor : ReceiveActor
    {
        private readonly IGameEventsPusher _gameEventPusher;
        private readonly IActorRef _gameController;

        public SignalRBridgeActor(IGameEventsPusher gameEventsPusher, IActorRef gameController)
        {
            _gameEventPusher = gameEventsPusher;
            _gameController = gameController;

            Receive<JoinGameMessage>(
                message =>
                {
                    _gameController.Tell(message);
                });

            Receive<AttackPlayerMessage>(
                message =>
                {
                    _gameController.Tell(message);
                });

            Receive<PlayerStatusMessage>(
                message =>
                {
                    _gameEventPusher.PlayerJoined(message.PlayerName, message.Health);
                });

            Receive<PlayerHealthChangedMessage>(
                message =>
                {
                    _gameEventPusher.UpdatePlayerHealth(message.PlayerName, message.Health);
                });


        }
    }
}
