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
           // GoogleCredential cred = GoogleCredential.FromFile(new StreamReader("betteryoutubefeed-service-key.json").ReadToEnd());
            UserCredential cred;
            string clientId = "993512608093-bds7qep5g83dbo5kldq1npnel8nluvqo.apps.googleusercontent.com";
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
            string clientId = "993512608093-bds7qep5g83dbo5kldq1npnel8nluvqo.apps.googleusercontent.com";
            string clientSecret = "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";
            string[] scopes = { Oauth2Service.ScopeConstants.UserinfoEmail, Oauth2Service.ScopeConstants.UserinfoProfile, Oauth2Service.ScopeConstants.Openid, Oauth2Service.Scope.UserinfoEmail, Oauth2Service.Scope.UserinfoProfile, Oauth2Service.Scope.Openid, YouTubeService.Scope.YoutubeReadonly};
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
                ApplicationName = "BetterYouTubeFeed",
            });
            var request = youtubeService.Subscriptions.List("snippet");
            request.Mine = true;
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
                ApplicationName = "BetterYouTubeFeed",
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
                ApplicationName = "BetterYouTubeFeed",
            });
            var request = youtubeService.Search.List("snippet");
            request.ChannelId = id;
            request.Order = SearchResource.ListRequest.OrderEnum.Date;
            request.Type = "video";
            request.MaxResults = 20;
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
                ApplicationName = "BetterYouTubeFeed",
            });
            var yt_request = youtubeService.Channels.List("snippet");
            yt_request.Mine = true;
            var yt_response = yt_request.Execute();
            if (yt_response.PageInfo.TotalResults != 0)
            {
                var data = yt_response.Items[0];
                return new Account(data.Id,userId,data.Snippet.Title,"","","",data.Snippet.CustomUrl,data.Snippet.Thumbnails.Medium.Url);
            }
            else
            {
                var OAuthService = new Oauth2Service(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                });
                var acc_request = OAuthService.Userinfo.Get();
                var acc_response = acc_request.ExecuteAsync().Result;
                return new Account(acc_response.Id, userId, acc_response.Name, acc_response.FamilyName, acc_response.GivenName, acc_response.Email, acc_response.Link, acc_response.Picture);
            }
        }
    }

}

