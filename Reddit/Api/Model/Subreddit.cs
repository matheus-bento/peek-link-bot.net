using System.Collections.Generic;

namespace PeekLinkBot.Reddit.Api.Model
{
    public class Subreddit
    {
        public bool DefaultSet { get; set; }
        public bool UserIsContributor { get; set; }
        public string BannerImg { get; set; }
        public bool RestrictPosting { get; set; }
        public bool UserIsBanned { get; set; }
        public bool FreeFormReports { get; set; }
        public string CommunityIcon { get; set; }
        public bool ShowMedia { get; set; }
        public string IconColor { get; set; }
        public bool UserIsMuted { get; set; }
        public string DisplayName { get; set; }
        public string HeaderImg { get; set; }
        public string Title { get; set; }
        public int Coins { get; set; }
        public IEnumerable<string> PreviousNames { get; set; }
        public bool Over18 { get; set; }
        public IEnumerable<int> IconSize { get; set; }
        public string PrimaryColor { get; set; }
        public string IconImg { get; set; }
        public string Description { get; set; }
        public string SubmitLinkLabel { get; set; }
        public int? HeaderSize { get; set; }
        public bool RestrictCommenting { get; set; }
        public int Subscribers { get; set; }
        public string SubmitTextLabel { get; set; }
        public bool IsDefaultIcon { get; set; }
        public string LinkFlairPosition { get; set; }
        public string DisplayNamePrefixed { get; set; }
        public string KeyColor { get; set; }
        public string Name { get; set; }
        public bool IsDefaultBanner { get; set; }
        public string Url { get; set; }
        public bool Quarintine { get; set; }
        public int? BannerSize { get; set; }
        public bool UserIsModerator { get; set; }
        public bool AcceptFollowers { get; set; }
        public string PublicDescription { get; set; }
        public bool LinkFlairEnabled { get; set; }
        public bool DisableContributorRequests { get; set; }
        public string SubredditType { get; set; }
        public bool UserIsSubscriber { get; set; }
    }
}
