using Akka.Actor;
using MovieStreaming.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStreaming.Actors
{
    public class PlaybackStatisticsActor : ReceiveActor
    {
        public PlaybackStatisticsActor()
        {
            Context.ActorOf(Props.Create<MoviePlayCounterActor>(), "MoviePlayCounter");
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(
                    execption =>
                    {
                        if(execption is SimulatedCorruptStateException)
                        {
                            return Directive.Restart;
                        }
                        if(execption is SimulatedTerribleMovieException)
                        {
                            return Directive.Resume;
                        }

                        return Directive.Restart;
                    }
                );

        }

        #region Lifecycle hooks

        protected override void PreStart()
        {
            ColorConsole.WriteWhite("PlaybackStatisticsActor PreStart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteWhite("PlaybackStatisticsActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteWhite($"PlaybackStatisticsActor PreRestart because: {reason.Message}");

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteWhite($"PlaybackStatisticsActor PostRestart because: {reason.Message}");

            base.PostRestart(reason);
        }
        #endregion
    }
}
