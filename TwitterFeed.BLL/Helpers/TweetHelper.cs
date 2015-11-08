using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterFeed.BLL.Helpers
{
    public static class TweetHelper
    {
        public static TwitterFeed.DTO.TwitterUser FormatUser(this string p_User)
        {
            try
            {
                if (string.IsNullOrEmpty(p_User)) throw new Exception("User string is null");

                TwitterFeed.DTO.TwitterUser User = new DTO.TwitterUser();

                if (p_User.IsWellFormedUserLine())
                {
                    //Ward follows Alan

                    //Alan follows Martin

                    //Ward follows Martin, Alan
                    string[] UserArr = p_User.Split(' ');

                    foreach (string item in UserArr)
                    {
                        if (item == UserArr.First()) //username
                        {
                            User.UserName = item;
                        }
                        else if (item != "follows")
                        {
                            if (!string.IsNullOrEmpty(User.Follows))
                            {
                                User.Follows += item;
                            }
                            else
                            {
                                User.Follows = item;
                            }
                        }
                    }

                    return User;
                }
                else
                {
                    throw new Exception("User not well formed");
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static List<TwitterFeed.DTO.TwitterUser> GetUsersFromFollowers(this List<TwitterFeed.DTO.TwitterUser> p_TwitterUsers, string p_Follows)
        {
            try
            {
                foreach (var follow in p_Follows.FollowsList())
                {
                    if (!p_TwitterUsers.Any(x => x.UserName == follow))
                    {
                        p_TwitterUsers.Add(new DTO.TwitterUser() { UserName = follow });
                    }
                }
                return p_TwitterUsers;
            }
            catch (Exception)
            {
                return p_TwitterUsers;
            }
        }

        //Each line of a well-formed user file contains a user, followed by the word 'follows' 
        //and then a comma separated list of users they follow.  
        //Where there is more than one entry for a user,  
        //consider the union of all these entries to determine the users they follow.
        public static bool IsWellFormedUserLine(this string p_UserLine)
        {
            try
            {
                return (p_UserLine != null && p_UserLine.Length > 0 && p_UserLine.Contains(" follows "));
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static TwitterFeed.DTO.Tweet FormatTweet(this string p_Tweet)
        {
            try
            {
                if (string.IsNullOrEmpty(p_Tweet)) throw new Exception("Tweet string is null");

                TwitterFeed.DTO.Tweet Tweet = new DTO.Tweet();

                if (p_Tweet.IsWellFormedTweet())
                {
                    //Alan> If you have a procedure with 10 parameters, you probably missed some.

                    //Ward> There are only two hard things in Computer Science: cache invalidation, naming things and off-by-1 errors.

                    //Alan> Random numbers should not be generated with a method chosen at random.
                    string[] TweetArr = p_Tweet.Split('>');

                    foreach (string item in TweetArr)
                    {
                        if (item == TweetArr.First()) //username
                        {
                            Tweet.UserName = item;
                        }
                        else
                        {
                            Tweet.TweetMessage = item.Trim();
                        }
                    }

                    return Tweet;
                }
                else
                {
                    throw new Exception("Tweet not well formed");
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static bool IsWellFormedTweet(this string p_Tweet)
        {
            try
            {
                return (p_Tweet != null && p_Tweet.Length > 0 && p_Tweet.Contains(">"));
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string[] FollowsList(this string p_Follows)
        {
            if (!string.IsNullOrEmpty(p_Follows))
            {
                return p_Follows.Split(',');
            }
            else
                return null;
        }

    }
}
