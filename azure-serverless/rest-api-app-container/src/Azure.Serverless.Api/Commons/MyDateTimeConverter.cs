using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace src.Azure.Serverless.Api.Commons
{
    public class MyDateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) =>
                DateTime.ParseExact(reader.GetString(),
                    "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);

        public override string ToString()
        {
            return base.ToString();
        }

        public override void Write(
            Utf8JsonWriter writer,
            DateTime dateTimeValue,
            JsonSerializerOptions options) =>
                writer.WriteStringValue(dateTimeValue.ToString(
                    "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture));
    }
}