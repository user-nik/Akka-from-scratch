using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStreaming.Common.Messages
{
    public class IncrementPlayCountMessage
    {
        public string MovieTitle { get; set; }

        public IncrementPlayCountMessage(string movieTitle)
        {
            MovieTitle = movieTitle;
        }
    }
}
