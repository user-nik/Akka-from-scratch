using Akka.Actor;
using MovieStreaming.Common;
using System;

namespace MovieStreaming.Remote
{
    class Program
    {
        private static ActorSystem MovieStreamingActorSystem;

        static void Main(string[] args)
        {
            ColorConsole.WriteLineGray("creating system in remote");

            MovieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");

            
            MovieStreamingActorSystem.WhenTerminated.Wait();
        }
    }
}
