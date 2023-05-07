using BetterYouTubeFeed.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetterYouTubeFeed.Models;

namespace BetterYouTubeFeedTests
{
    [TestClass]
    public class BYTFContextTests : BYTFContext
    {
        [TestMethod]
        public void UpdateChannels_UpdatesDatabaseContext()
        {
            UpdateChannels();
            UpdateVideos();
            var channels = from o in this.Channels select o;
            var videos = from o in this.Videos select o;
            Assert.IsNotNull(channels);
            Assert.IsNotNull(videos);
        }
        [TestMethod]
        public void Drop_UpdatesDatabaseContext()
        {
            Drop();
            var accounts = from o in this.Accounts select o;
            var channels = from o in this.Channels select o;
            var videos = from o in this.Videos select o;
            Assert.IsNotNull(accounts);
            Assert.IsNotNull(channels);
            Assert.IsNotNull(videos);
        }
    }
}
