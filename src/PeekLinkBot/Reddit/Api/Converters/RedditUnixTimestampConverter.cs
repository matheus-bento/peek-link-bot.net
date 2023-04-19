using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PeekLinkBot.Reddit.Api.Converters
{
    /// <summary>
    ///     Reddit API's timestamps are floating point numbers and, for some reason, 
    ///     Newtonsoft's built-in UnixDateTimeConverter does not work with floating-point numbers
    /// </summary>
    public class RedditUnixTimestampConverter : DateTimeConverterBase
    {
        private readonly DateTime _unixEpoch = new DateTime(1970, 1, 1);

        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            
            if (reader.Value != null)
            {
                double unixTimestamp = (double)reader.Value;
                return this._unixEpoch.AddSeconds(unixTimestamp);
            }
            else
            {
                return null;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            double unixTimestamp;

            if (value is DateTime)
            {
                unixTimestamp = ((DateTime)value).Subtract(this._unixEpoch).TotalSeconds;
            }
            else
            {
                throw new Exception("Expected a date value");
            }

            writer.WriteValue(unixTimestamp);
        }
    }
}
