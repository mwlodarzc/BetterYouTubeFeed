using BetterYouTubeFeed;
using BetterYouTubeFeed.Models;
namespace BetterYouTubeFeedTests
{
    [TestClass]
    public class YouTubeDataApiTests : YouTubeDataAPI
    {
        [TestMethod]
        public void StringKey_ReturnsDifferentKeys()
        {
            string Key1 = StringKey();
            string Key2 = StringKey();
            Assert.AreNotEqual(Key1, Key2);
        }

        [TestMethod]
        public void Authenticate_ReturnsCred()
        {
            var cred = Authenticate();
            Assert.IsNotNull(cred);
        }
        [TestMethod]
        public void GetInfo_ReturnsAccountInfoSubscribtionsChannelAndVideo()
        {
            Account accountInfo = GetAccountInfo();
            List<string> ID = GetSubsctiptionsID(accountInfo);
            Channel channel = GetChannelInfo(accountInfo, ID[0]);
            ICollection<Video> videos = GetVideos(accountInfo, ID[0]);

            Assert.IsNotNull(accountInfo);
            Assert.IsNotNull(ID);
            Assert.IsNotNull(channel);
            Assert.IsNotNull(videos);
        }
    }
}