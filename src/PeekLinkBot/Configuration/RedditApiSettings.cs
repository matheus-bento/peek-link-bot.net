using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace PeekLinkBot.Configuration
{
    public static class RedditApiSettings
    {
        private static JsonSerializerSettings _serializerSettings = null;

        public static JsonSerializerSettings SerializerSettings
        {
            get
            {
                if (_serializerSettings == null)
                    _serializerSettings = new JsonSerializerSettings
                    {
                        ContractResolver = new DefaultContractResolver
                        {
                            NamingStrategy = new SnakeCaseNamingStrategy()
                        }
                    };

                return _serializerSettings;
            }
        }
    }
}
