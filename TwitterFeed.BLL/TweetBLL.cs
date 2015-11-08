using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterFeed.BLL.Helpers;

namespace TwitterFeed.BLL
{
    /// <summary>
    /// This is where the magic happens
    /// </summary>
    public class TweetBLL
    {
        public List<DTO.Tweet> GetTweets(string p_TweetFilePath, Encoding p_Encoding)
        {
            var FileReader = TwitterFeed.DAL.FileHelper.FileReader.Instance;

            List<DTO.Tweet> Tweets = new List<DTO.Tweet>();

            List<string> TweetsLines = FileReader.ReadFromFile(p_TweetFilePath, p_Encoding);

            foreach (string line in TweetsLines)
            {
                DTO.Tweet TweetDTO = line.FormatTweet();

                if (TweetDTO != null)
                {
                    if (!Tweets.Any(x => x.UserName == TweetDTO.UserName && x.TweetMessage == TweetDTO.TweetMessage))
                    {
                        Tweets.Add(TweetDTO);
                    }
                }
            }

            return Tweets;
        }

        public List<DTO.TwitterUser> GetUsers(string p_UserFilePath, Encoding p_Encoding)
        {
            var FileReader = TwitterFeed.DAL.FileHelper.FileReader.Instance;

            List<DTO.TwitterUser> TwitterUsers = new List<DTO.TwitterUser>();

            List<string> Users = FileReader.ReadFromFile(p_UserFilePath, p_Encoding);

            foreach (string user  in Users)
            {
                DTO.TwitterUser UserDTO = user.FormatUser();

                if (UserDTO != null)
                {
                    if (!TwitterUsers.Any(x => x.UserName == UserDTO.UserName))
                    {
                        TwitterUsers.Add(UserDTO);
                    }
                    else if (TwitterUsers.Any(x => x.UserName == UserDTO.UserName))
                    {
                        var ExistingUser = TwitterUsers.First(x => x.UserName == UserDTO.UserName);
                        
                        if (UserDTO.Follows.Length > ExistingUser.Follows.Length)
                        {
                            TwitterUsers.Remove(ExistingUser);
                            TwitterUsers.Add(UserDTO);
                        }
                    }

                    if (!string.IsNullOrEmpty(UserDTO.Follows))
                    {
                        TwitterUsers.GetUsersFromFollowers(UserDTO.Follows);
                    }
                }
            }

            return TwitterUsers.OrderBy(x => x.UserName).ToList();
        }
    }
}
