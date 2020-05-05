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
        public PlaybackActor()
        {
            Context.ActorOf(Props.Create<UserCoordinator>(), "UserCoordinator");
            Context.ActorOf(Props.Create<PlaybackStatisticsActor>(), "PlaybackStatisticsActor");
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
