using Akka.Actor;
using MovieStreaming.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStreaming.Actors
{
    public class PlaybackActor : ReceiveActor
    {
        private readonly IActorRef _userCoordinator;
        private readonly IActorRef _statistics;

        public PlaybackActor()
        {
            _userCoordinator = Context.ActorOf(Props.Create<UserCoordinatorActor>(), "UserCoordinator");
            _statistics = Context.ActorOf(Props.Create<PlaybackStatisticsActor>(), "PlaybackStatistics");


            Receive<PlayMovieMessage>(message =>
            {
                ColorConsole.WriteLineGreen(
                    $"PlaybackActor received" +
                    $"PlayMovieMessage '{message.MovieTitle}' for user {message.UserId}");

                _userCoordinator.Tell(message);
            });
        }
        #region hooks

        protected override void PreStart()
        {
            ColorConsole.WriteLineGreen("Playback prestart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineGreen("Playback PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineGreen("Playback PreRestart " + reason);

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLineGreen("Playback PostRestart " + reason);

            base.PostRestart(reason);
        }

        #endregion
    }
}
