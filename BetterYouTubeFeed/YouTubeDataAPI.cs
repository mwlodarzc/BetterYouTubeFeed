using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Navigation;
using static System.Net.WebRequestMethods;
using System.Threading;
using Microsoft.Identity.Client;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util;
using Google.Apis.YouTube;
using Google.Apis.YouTube.v3;
using Google.Apis.Services;
using Google.Apis.Auth;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.IO;
using Microsoft.IdentityModel.Tokens;
using BetterYouTubeFeed.Models;
using System.Runtime.CompilerServices;
using Google.Apis.Oauth2.v2;
using Google.Apis.Util.Store;

namespace BetterYouTubeFeed
{
    class YouTubeDataAPI
    {
        static string clientId = "32650836834-tvvdl85dgdo9jdk51q4bg82a8ou6ev3k.apps.googleusercontent.com";
        static string clientSecret = "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";
        private static string StringKey()
        {
            int length = 7;
            StringBuilder str_build = new StringBuilder();
            Random random = new Random();
            char letter;
            for (int i = 0; i < length; i++)
            {
                double flt = random.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * flt));
                letter = Convert.ToChar(shift + 65);
                str_build.Append(letter);
            }
            return str_build.ToString();
        }
        private static UserCredential Authenticate(string userId)
        {
            UserCredential cred;
            string clientId = "893351996823-hq7k4bv0daea7egff9cc6ruru5pajqsj.apps.googleusercontent.com";
            string clientSecret = "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";
            string[] scopes = {  YouTubeService.Scope.YoutubeReadonly };
            cred = GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = clientId,
                    ClientSecret = clientSecret,
                },
                scopes, userId, CancellationToken.None,new FileDataStore("YouTubeDataAPI.Auth.Store")).Result;
            if (cred.Token.IsExpired(SystemClock.Default))
                cred.RefreshTokenAsync(CancellationToken.None).Wait();
            return cred;
            
        }

        private static (UserCredential,string) Authenticate()
        {
            UserCredential cred;
            string[] scopes = { Oauth2Service.Scope.UserinfoEmail, Oauth2Service.Scope.UserinfoProfile, Oauth2Service.Scope.Openid, YouTubeService.Scope.YoutubeReadonly};
            string userId = StringKey();
            cred = GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = clientId,
                    ClientSecret = clientSecret,
                },
                scopes, userId, CancellationToken.None, new FileDataStore("YouTubeDataAPI.Auth.Store")).Result;
            if (cred.Token.IsExpired(SystemClock.Default))
                cred.RefreshTokenAsync(CancellationToken.None).Wait();
            return (cred, userId);
        }

        public static List<string> GetSubsctiptionsID(Account account)
        {
            UserCredential credential = Authenticate(account.AuthId);
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "BYTF",
            });
            var request = youtubeService.Subscriptions.List("snippet");
            request.Mine = true;
            request.MaxResults = 3;
            var response = request.Execute();
            List<string> result = new List<string>();
            foreach (var item in response.Items)
                result.Add(item.Snippet.ResourceId.ChannelId);
            return result;
        }

        public static Channel GetChannelInfo(Account account, string id)
        {
            UserCredential credential = Authenticate(account.AuthId);
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "BYTF",
            });
            var request = youtubeService.Channels.List("snippet");
            request.Id = id;
            var response = request.Execute();
            var data = response.Items[0];
            return new Channel(data.Id,data.Snippet.Title,data.Snippet.CustomUrl,data.Snippet.Thumbnails.Medium.Url,data.Snippet.Description,account.AccountId);

        }

        public static ICollection<Video> GetVideos(Account account, string id)
        {
            UserCredential credential = Authenticate(account.AuthId);

            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "BYTF",
            });
            var request = youtubeService.Search.List("snippet");
            request.ChannelId = id;
            request.Order = SearchResource.ListRequest.OrderEnum.Date;
            request.Type = "video";
            request.MaxResults = 1;
            var response = request.Execute();
            ICollection<Video> result = new List<Video>();
            foreach (var item in response.Items)
                result.Add(new Video(item.Id.VideoId, item.Snippet.Title,DateTime.Parse(item.Snippet.PublishedAtRaw).ToString(), DateTime.Now.ToString(),item.Snippet.Thumbnails.Medium.Url,id));
                
            return result;

        }


        public static Account GetAccountInfo()
        {
            (UserCredential credential, string userId) = Authenticate();
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "BYTF",
            });
            var yt_request = youtubeService.Channels.List("snippet");
            yt_request.Mine = true;
            var yt_response = yt_request.Execute();
            if (yt_response.PageInfo.TotalResults != 0)
            {
                var data = yt_response.Items[0];
                return new Account(data.Id,"channel",userId,data.Snippet.Title,"","","",data.Snippet.CustomUrl,data.Snippet.Thumbnails.Medium.Url);
            }
            else
            {
                var OAuthService = new Oauth2Service(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                });
                var acc_request = OAuthService.Userinfo.Get();
                var acc_response = acc_request.ExecuteAsync().Result;
                return new Account(acc_response.Id,"account", userId, acc_response.Name, acc_response.FamilyName, acc_response.GivenName, acc_response.Email, acc_response.Link, acc_response.Picture);
            }
        }
    }

}

