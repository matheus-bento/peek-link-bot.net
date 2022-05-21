using Newtonsoft.Json;
using PeekLinkBot.Reddit.Api.Converters;
using System;
using System.Collections.Generic;

namespace PeekLinkBot.Reddit.Api.Model
{
    public class RedditUserIdentity
    {
        public bool IsEmployee { get; set; }
        public bool SeenLayoutSwitch { get; set; }
        public bool HasVisitedNewProfile { get; set; }
        public bool PrefNoProfanity { get; set; }
        public bool HasExternalAccount { get; set; }
        public bool? PrefGeopopular { get; set; }
        public bool SeenRedesignModal { get; set; }
        public bool PrefShowTrending { get; set; }
        public Subreddit Subreddit { get; set; }
        public bool PrefShowPresence { get; set; }
        public string SnoovatarImg { get; set; }
        public int? SnoovatarSize { get; set; }
        [JsonConverter(typeof(RedditUnixTimestampConverter))]
        public DateTime? GoldExpiration { get; set; }
        public bool HasGoldSubscription { get; set; }
        public bool IsSponsor { get; set; }
        public int NumFriends { get; set; }
        public object Features { get; set; }
        public bool CanEditName { get; set; }
        public bool Verified { get; set; }
        public bool? NewModmailExists { get; set; }
        public bool PrefAutoplay { get; set; }
        public int Coins { get; set; }
        public bool HasPaypalSubscription { get; set; }
        public bool HasSubscribedToPremium { get; set; }
        public string Id { get; set; }
        public bool HasStripeSubscription { get; set; }
        public string OauthClientId { get; set; }
        public bool CanCreateSubreddit { get; set; }
        public bool Over18 { get; set; }
        public bool IsGold { get; set; }
        public bool IsMod { get; set; }
        public int AwarderKarma { get; set; }
        [JsonConverter(typeof(RedditUnixTimestampConverter))]
        public DateTime? SuspensionExpirationUtc { get; set; }
        public bool HasVerifiedEmail { get; set; }
        public bool IsSuspended { get; set; }
        public bool PrefVideooAutoplay { get; set; }
        public bool HasAndrioidSuubscription { get; set; }
        public bool InRedesignBeta { get; set; }
        public string IconImg { get; set; }
        public bool HasModMail { get; set; }
        public bool PrefNightmode { get; set; }
        public int AwardeeKarma { get; set; }
        public bool HideFromRobots { get; set; }
        public bool PasswordSet { get; set; }
        public int LinkKarma { get; set; }
        public bool ForcePasswordReset { get; set; }
        public int TotalKarma { get; set; }
        public bool SeenGiveAwardTooltip { get; set; }
        public int InboxCount { get; set; }
        public bool SeenPremiumAdblockModal { get; set; }
        public bool PrefTopKarmaSubreddits { get; set; }
        public bool HasMail { get; set; }
        public bool PrefShowSnoovatar { get; set; }
        public string Name { get; set; }
        public int PrefClickgadget { get; set; }
        [JsonConverter(typeof(RedditUnixTimestampConverter))]
        public DateTime Created { get; set; }
        public int GoldCreddits { get; set; }
        [JsonConverter(typeof(RedditUnixTimestampConverter))]
        public DateTime CreatedUTC { get; set; }
        public bool HasIosSubscription { get; set; }
        public bool PrefShowTwitter { get; set; }
        public bool InBeta { get; set; }
        public int CommentKarma { get; set; }
        public bool AcceptFollowers { get; set; }
        public bool HasSubscribed { get; set; }
        public IEnumerable<object> LinkedIdentities { get; set; }
        public bool SeenSubredditChatFtux { get; set; }
    }
}
