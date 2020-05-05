using Akka.Actor;
using MovieStreaming.Common.Messages;
using System;

namespace MovieStreaming.Common.Actors
{
    public class UserActor : ReceiveActor
    {
        private readonly int _userId;
        private string _currentWatching;

        public UserActor(int userId)
        {
            _userId = userId;

            Stopped();
        }

        private void Playing()
        {
            Receive<PlayMovieMessage>(message => ColorConsole.WriteLineRed(
                       "cannot start playing another movie"));

            Receive<StopMovieMessage>(
                message => StopPlayingCurrentMovie());

            ColorConsole.WriteLineYellow($"useractor {_userId} has become playing");
        }

        private void Stopped()
        {
            Receive<PlayMovieMessage>(message => StartPlayingMovie(message.MovieTitle));
            Receive<StopMovieMessage>(
                message => ColorConsole.WriteLineRed("cannot stop if nothing is playing"));

            ColorConsole.WriteLineYellow($"useractor {_userId} has become stopped");
        }


        private void StopPlayingCurrentMovie()
        { 
            ColorConsole.WriteLineYellow($"Actor {_userId} is watching {_currentWatching}");

            _currentWatching = null;

            Become(Stopped);
        }
        

        private void StartPlayingMovie(string movieTitle)
        {
            _currentWatching = movieTitle;

            ColorConsole.WriteLineYellow($"Actor {_userId} is watching {_currentWatching}");

            Context.ActorSelection("/user/Playback/PlaybackStatistics/MoviePlayCounter")
                .Tell(new IncrementPlayCountMessage(movieTitle));

            Become(Playing);
        }

        #region hooks
        protected override void PreStart()
        {
            ColorConsole.WriteLineYellow($"Actor {_userId} prestart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineYellow($"Actor {_userId} PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineYellow($"Actor {_userId} PreRestart " + reason);

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLineYellow($"Actor {_userId} PostRestart " + reason);

            base.PostRestart(reason);
        }
        #endregion
    }
}
