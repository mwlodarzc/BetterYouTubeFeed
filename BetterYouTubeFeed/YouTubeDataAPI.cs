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

namespace BetterYouTubeFeed
{
    class YouTubeDataAPI
    {
        public static UserCredential? credential;
        public static async Task Authenticate()
        {
            UserCredential cred;
            string clientId = "993512608093-bds7qep5g83dbo5kldq1npnel8nluvqo.apps.googleusercontent.com";
            string clientSecret = "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";
            string[] scopes = { YouTubeService.Scope.YoutubeReadonly };
                cred = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = clientId,
                    ClientSecret = clientSecret,
                },
                scopes, "user", CancellationToken.None);
            var test = cred.GetType;
            if (cred.Token.IsExpired(SystemClock.Default))
                cred.RefreshTokenAsync(CancellationToken.None).Wait();
            credential = cred;
        }

        public static List<string> GetSubsctiptionsID()
        {
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

        public static Channel GetChannelInfo(string id)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "BetterYouTubeFeed",
            });
            var request = youtubeService.Channels.List("snippet");
            request.Id = id;
            var response = request.Execute();
            var data = response.Items[0];
            return new Channel(data.Id,data.Snippet.Title,data.Snippet.CustomUrl,data.Snippet.Description);

        }

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
}
