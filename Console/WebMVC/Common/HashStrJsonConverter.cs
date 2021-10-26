using HashidsNet;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebMVC.Common
{
    public class HashStrJsonConverter : JsonConverter<string>
    {
        Hashids hashids = new Hashids("key");//加盐
        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var str = JsonSerializer.Deserialize<string>(ref reader, options);

            return hashids.DecodeHex(str);
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, hashids.EncodeHex(value), options);
        }
    }
}
