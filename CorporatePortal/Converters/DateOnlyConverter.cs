using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Globalization;

namespace CorporatePortal.Converters
{
    public class DateOnlyConverter : JsonConverter
    {
        private readonly string _dateFormat;

        public DateOnlyConverter(string dateFormat)
        {
            _dateFormat = dateFormat;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var date = (DateOnly)value;
            writer.WriteValue(date.ToString(_dateFormat));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var date = (string)reader.Value;
            return DateOnly.ParseExact(date, _dateFormat, CultureInfo.InvariantCulture);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateOnly);
        }
    }
}
