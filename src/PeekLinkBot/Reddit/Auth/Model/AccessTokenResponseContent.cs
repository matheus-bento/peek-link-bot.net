namespace PeekLinkBot.Reddit.Auth.Model
{
    /// <summary>
    ///     Response from the /api/v1/access_token endpoint that returns the access token required by
    ///     https://oauth.reddit.com endpoints
    /// </summary>
    public class AccessTokenResponseContent
    {
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public int ExpiresIn { get; set; }
        public string Scope { get; set; }
    }
}
