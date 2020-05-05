using Akka.Actor;
using MovieStreaming.Messages;
using System;

namespace MovieStreaming.Actors
{
    public class UserActor : ReceiveActor
    {

        private string _currentWatching;

        public UserActor()
        {
            Console.WriteLine("creating actor");

            ColorConsole.WriteLineCyan("setting inital behavior stopped");
            Stopped();
        }

        private void Playing()
        {
            Receive<PlayMovieMessage>(message => ColorConsole.WriteLineRed(
                       "cannot start playing another movie"));

            Receive<StopMovieMessage>(
                message => StopPlayingCurrentMovie());

            ColorConsole.WriteLineCyan("useractor has become playing");
        }

        private void Stopped()
        {
            Receive<PlayMovieMessage>(message => StartPlayingMovie(message.MovieTitle));
            Receive<StopMovieMessage>(
                message => ColorConsole.WriteLineRed("cannot stop if nothing is playing"));

            ColorConsole.WriteLineCyan("useractor has become stopped");
        }


        private void StopPlayingCurrentMovie()
        { 
            ColorConsole.WriteLineYellow($"user is watching {_currentWatching}");

            _currentWatching = null;

            Become(Stopped);
        }
        

        private void StartPlayingMovie(string movieTitle)
        {
            _currentWatching = movieTitle;

            ColorConsole.WriteLineYellow($"user is watching {_currentWatching}");

            Become(Playing);
        }

        #region hooks
        protected override void PreStart()
        {
            ColorConsole.WriteLineGreen("Actor prestart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineGreen("Actor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineGreen("Actor PreRestart " + reason);

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLineGreen("Actor PostRestart " + reason);

            base.PostRestart(reason);
        }
        #endregion
    }
}
