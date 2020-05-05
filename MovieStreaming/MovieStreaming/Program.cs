using Akka.Actor;
using MovieStreaming.Actors;
using MovieStreaming.Messages;
using System;
using System.Threading;

namespace MovieStreaming
{
    class Program
    {
        public static ActorSystem MovieStreamingActorSystem { get; set; }
        static void Main(string[] args)
        {
            MovieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");
            Console.WriteLine("created");

            IActorRef userActorRef = MovieStreamingActorSystem.ActorOf(
                Props.Create<PlaybackActor>(), "Playback");

            do
            {
                ShortPause();

                Console.WriteLine();
                ColorConsole.WriteLineGray("enter a command and hit");

                var command = Console.ReadLine();

                if (command.StartsWith("play"))
                {
                    int userId = int.Parse(command.Split(',')[1]);
                    string movieTitle = command.Split(',')[2];

                    var message = new PlayMovieMessage(movieTitle, userId);
                    MovieStreamingActorSystem.ActorSelection("/user/Playback/UserCoordinator").Tell(message);
                }

                if (command.StartsWith("stop"))
                {
                    int userId = int.Parse(command.Split(',')[1]);

                    var message = new StopMovieMessage(userId);
                    MovieStreamingActorSystem.ActorSelection("/user/Playback/UserCoordinator").Tell(message);
                }

                if (command.StartsWith("exit"))
                {
                    MovieStreamingActorSystem.Terminate();
                    MovieStreamingActorSystem.WhenTerminated.Wait();

                    Console.WriteLine("Terminated");
                    Console.ReadKey();
                    Environment.Exit(1);
                }

            } while (true);


        }

        private static void ShortPause()
        {
            Thread.Sleep(450);
        }
    }
}
