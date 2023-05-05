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
        public static async Task<UserCredential> Authenticate()
        {
            UserCredential cred;
            string clientId = "993512608093-bds7qep5g83dbo5kldq1npnel8nluvqo.apps.googleusercontent.com";
            string clientSecret = "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";
            string[] scopes = {  YouTubeService.Scope.YoutubeReadonly , Oauth2Service.ScopeConstants.UserinfoEmail, Oauth2Service.ScopeConstants.UserinfoProfile, Oauth2Service.ScopeConstants.Openid, Oauth2Service.Scope.UserinfoEmail, Oauth2Service.Scope.UserinfoProfile, Oauth2Service.Scope.Openid };
            cred = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = clientId,
                    ClientSecret = clientSecret,
                },
                scopes, "user", CancellationToken.None);
            if (cred.Token.IsExpired(SystemClock.Default))
                cred.RefreshTokenAsync(CancellationToken.None).Wait();
            return cred;
        }

        public static async Task<UserCredential> Authenticate(string userId)
        {
            UserCredential cred;
            string clientId = "993512608093-bds7qep5g83dbo5kldq1npnel8nluvqo.apps.googleusercontent.com";
            string clientSecret = "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";
            string[] scopes = { YouTubeService.Scope.YoutubeReadonly, Oauth2Service.ScopeConstants.UserinfoEmail, Oauth2Service.ScopeConstants.UserinfoProfile, Oauth2Service.ScopeConstants.Openid, Oauth2Service.Scope.UserinfoEmail, Oauth2Service.Scope.UserinfoProfile, Oauth2Service.Scope.Openid};
            cred = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = clientId,
                    ClientSecret = clientSecret,
                },
                scopes, userId, CancellationToken.None);
            if (cred.Token.IsExpired(SystemClock.Default))
                cred.RefreshTokenAsync(CancellationToken.None).Wait();
            return cred;
        }

        public static List<string> GetSubsctiptionsID(Account account)
        {
            UserCredential credential = Authenticate(account.AuthId).Result;
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
            UserCredential credential = Authenticate(account.AuthId).Result;
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "BetterYouTubeFeed",
            });
            var request = youtubeService.Channels.List("snippet");
            request.Id = id;
            var response = request.Execute();
            var data = response.Items[0];
            return new Channel(data.Id,data.Snippet.Title,data.Snippet.CustomUrl,data.Snippet.Thumbnails.Standard.Url,data.Snippet.Description,account.AccountId);

        }

        public static ICollection<Video> GetVideos(Account account, string id)
        {
            UserCredential credential = Authenticate(account.AuthId).Result;

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

        public static Account GetAccountInfo(UserCredential credential)
        {
            var OAuthService = new Oauth2Service(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
            });
            var request = OAuthService.Userinfo.Get();
            var response = request.ExecuteAsync().Result;
            return new Account(response.Id, credential.UserId, response.Name,response.FamilyName,response.GivenName,response.Email, response.Link,response.Picture);
        }   
    }

    /*
    public static ICollection<Video> GetComunityPosts(string id)
    {
        var youtubeService = new YouTubeService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = "BetterYouTubeFeed",
        });
        var request = youtubeService.Search.List("Snippet");
        request.ChannelId = id;
        request.Order = SearchResource.ListRequest.OrderEnum.Date;
        request.Type = "video";
        request.MaxResults = 20;
        var response = request.Execute();
        ICollection<Video> result = new List<Video>();
        foreach (var item in response.Items)
            result.Add(new Video(item.Snippet.Title, item.Id.VideoId, item.Snippet.PublishedAtRaw, DateTime.Now.ToString()));
        return result;
    }*/

    /*
  [GoogleScopedAuthorize]
  [GoogleScopedAuthorize(DriveService.ScopeConstants.DriveReadonly)]
  public async Task<IActionResult> DriveFileList([FromServices] IGoogleAuthProvider auth)
  {
      GoogleCredential cred = await auth.GetCredentialAsync();
      var service = new DriveService(new BaseClientService.Initializer
      {
          HttpClientInitializer = cred
      });
      var files = await service.Files.List().ExecuteAsync();
      var fileNames = files.Files.Select(x => x.Name).ToList();
      return View(fileNames);
  }


  var result = GetSubscriptionsAsync().Result;

  private static async Task<dynamic> GetSubscriptionsAsync()
  {
      var parameter = new Dictionary<string, string> {
          ["key"] = ConfigurationManager.AppSettings["ApiKey"],
          ["part"] = "snippet",
          ["fields"] = "items/snippet(title,description)",
      };

      string baseURL = "https://www.googleapis.com/youtube/v3/subscriptions?";
      string fullURL = MakeURL_fromQuery(baseURL, parameter);
      var result = await new HttpClient().GetStringAsync(fullURL);

      if (result != null)
          return JsonConvert.DeserializeObject(result);
  }   return null;

  private static string MakeURL_fromQuery(string baseURL, Dictionary<string, string> parameter)
  {
      if (string.IsNullOrEmpty(baseURL))
          throw new ArgumentNullException(nameof(baseURL));
      if (parameter == null || parameter.Count == 0)
          return baseURL;
      return parameter.Aggregate(baseURL, (accumulated, kvp) => string.Format($"{accumulated}{kvp.Key}={kvp.Value}"));
}*/
}

