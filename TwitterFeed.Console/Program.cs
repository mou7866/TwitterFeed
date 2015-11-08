using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterFeed.BLL.Helpers;

namespace TwitterFeed.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Welcome to Twitter Feed Simulator");
            System.Console.WriteLine();
            System.Console.WriteLine(@"Please make sure the User.txt and Tweet.txt files are copied to C:\Temp folder");
            System.Console.WriteLine();
            System.Console.WriteLine("Press enter to continue......");

            var EnterKey = System.Console.ReadKey();

            if (EnterKey.Key == ConsoleKey.Enter)
            {
                System.Console.Clear();
                System.Console.ForegroundColor = ConsoleColor.Blue;
                TwitterFeed.BLL.TweetBLL TwitterBLL = new BLL.TweetBLL();
                List<TwitterFeed.DTO.TwitterUser> Users = TwitterBLL.GetUsers(@"C:\Temp\user.txt", ASCIIEncoding.ASCII);
                List<TwitterFeed.DTO.Tweet> Tweets = TwitterBLL.GetTweets(@"C:\Temp\tweet.txt", ASCIIEncoding.ASCII);

                //Alan

                //@Alan: If you have a procedure with 10 parameters, you probably missed some.

                //@Alan: Random numbers should not be generated with a method chosen at random.

                //Martin

                //Ward

                //@Alan: If you have a procedure with 10 parameters, you probably missed some.

                //@Ward: There are only two hard things in Computer Science: cache invalidation, naming things and off-by-1 errors.

                //@Alan: Random numbers should not be generated with a method chosen at random.​

                foreach (DTO.TwitterUser user in Users)
                {
                    System.Console.WriteLine(user.ToString());
                    System.Console.WriteLine();
                    foreach (DTO.Tweet tweet in Tweets.Where(x => x.UserName == user.UserName))
                    {
                        System.Console.WriteLine(tweet.ToString());
                        System.Console.WriteLine();
                    }

                    string[] FollowsList = user.Follows.FollowsList();
                    if (FollowsList != null && FollowsList.Length > 0)
                    {
                        foreach (string follow in FollowsList)
                        {
                            foreach (DTO.Tweet followTweet in Tweets.Where(x => x.UserName == follow))
                            {
                                System.Console.WriteLine(followTweet);
                                System.Console.WriteLine();
                            }
                        }
                    }
                }

                System.Console.ReadKey();
            }
        }

    }
}
