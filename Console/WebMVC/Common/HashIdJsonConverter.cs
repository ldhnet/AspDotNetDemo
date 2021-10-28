using HashidsNet;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebMVC.Common
{
    public class HashIdJsonConverter : JsonConverter<int>
    {
        Hashids hashids = new Hashids("key",6);//加盐
        public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var str = JsonSerializer.Deserialize<string>(ref reader, options);

            return hashids.Decode(str)[0];
        }

        public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, hashids.Encode(value), options);
        }
    }
}
