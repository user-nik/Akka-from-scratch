using Akka.Actor;
using MovieStreaming.Actors;
using MovieStreaming.Messages;
using System;

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
                Props.Create<UserActor>(), "UserActor");

            do
            {
                ShortPause();

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                ColorConsole.WriteLineGray("enter a command and hit");

                var command = Console.ReadLine();

                if (command.StartsWith("play"))
                {
                    int userId = int.Parse(command.Split(',')[1]);
                    string movieTitle = command.Split(',')[2];

                    var message = new PlayMovieMessage(movieTitle, userId);
                    MovieStreamingActorSystem.ActorSelection("/user/Playback/UserCooridnator").Tell(message);
                }

                if (command.StartsWith("stop"))
                {
                    int userId = int.Parse(command.Split(',')[1]);

                    var message = new StopMovieMessage(userId);
                    MovieStreamingActorSystem.ActorSelection("/user/Playback/UserCooridnator").Tell(message);
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



            Console.ReadKey();
            Console.WriteLine("sending play moviemessage Akka: the Movie");
            userActorRef.Tell(new PlayMovieMessage("Akka: the Movie", 42));

            Console.ReadKey();
            Console.WriteLine("sending play moviemessage Recall");
            userActorRef.Tell(new PlayMovieMessage("Recall", 99));

            Console.ReadKey();
            Console.WriteLine("sending a stopMovieMessage");
            userActorRef.Tell(new StopMovieMessage());
            //userActorRef.Tell(PoisonPill.Instance);

            Console.ReadKey();
            Console.WriteLine("sending a stopMovieMessage");
            userActorRef.Tell(new StopMovieMessage());

            

            Console.ReadKey();

            
        }
    }
}
