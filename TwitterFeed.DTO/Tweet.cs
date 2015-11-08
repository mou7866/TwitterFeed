using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterFeed.DTO
{
    public class Tweet
    {
        public string UserName { get; set; }

        private string _tweet = string.Empty;
        public string TweetMessage 
        {
            get
            {
                return _tweet;
            }
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Length > 140)
                {
                    _tweet = value.Substring(0, 140);
                }
                else
                    _tweet = value;
            }
        }

        public override string ToString()
        {
            //@Alan: If you have a procedure with 10 parameters, you probably missed some.
            return "\t @" + UserName + " : " + TweetMessage; 
        }
    }
}
