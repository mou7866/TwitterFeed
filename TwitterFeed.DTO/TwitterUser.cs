using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterFeed.DTO
{
    public class TwitterUser
    {
        public string UserName { get; set; }

        private string _follows = string.Empty;
        public string Follows
        {
            get
            {
                return _follows;
            }
            set
            {
                _follows = value;
            }
        }

        public string Followers { get; set; }

        public override string ToString()
        {
            return UserName;
        }


    }
}
