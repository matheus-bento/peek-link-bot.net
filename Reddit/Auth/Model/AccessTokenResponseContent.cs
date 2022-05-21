namespace PeekLinkBot.Reddit.Auth.Model
{
    public class AccessTokenResponseContent
    {
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public int ExpiresIn { get; set; }
        public string Scope { get; set; }
    }
}
