using Akka.Actor;
using MovieStreaming.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStreaming.Actors
{
    public class UserCoordinatorActor : ReceiveActor
    {
        private readonly Dictionary<int, IActorRef> _users;

        public UserCoordinatorActor()
        {
            _users = new Dictionary<int, IActorRef>();

            Receive<PlayMovieMessage>(
                message =>
                {
                    CreateChildUserIfNotExists(message.UserId);

                    IActorRef childActorRef = _users[message.UserId];

                    childActorRef.Tell(message);
                });

            Receive<StopMovieMessage>(
                message =>
                {
                    CreateChildUserIfNotExists(message.UserId);

                    IActorRef childActorRef = _users[message.UserId];

                    childActorRef.Tell(message);
                });
        }

        private void CreateChildUserIfNotExists(int userId)
        {
            if (!_users.ContainsKey(userId))
            {
                IActorRef newChildActorRef =
                    Context.ActorOf(Props.Create(() => new UserActor(userId), "user" + userId));

                _users.Add(userId, newChildActorRef);

                ColorConsole.WriteLineCyan($"UserCoordinatorActor for user {userId} user count {_users.Count}");
            }
        }

        #region hooks

        protected override void PreStart()
        {
            ColorConsole.WriteLineCyan(" UserCoordinatorActor prestart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineCyan(" UserCoordinatorActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineCyan(" UserCoordinatorActor PreRestart " + reason);

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLineCyan(" UserCoordinatorActor PostRestart " + reason);

            base.PostRestart(reason);
        }

        #endregion
    }
}
