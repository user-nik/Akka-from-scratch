using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.ActorModel.Messages
{
    public class JoinGameMessage
    {

        public string PlayerName { get; private set; }

        public JoinGameMessage(string playerName)
        {
            PlayerName = playerName;
        }
    }
}
