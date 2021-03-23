using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication
{
    [JsonConverter(typeof(WhitelistSerializer))]
    public class Intern
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Duration { get; set; }
        public char Type { get; set; }
        public string Department { get; set; }
        public string Organization { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
    }

    public class WhitelistSerializer : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var name = value as Intern;
            writer.WriteStartObject();

            writer.WritePropertyName("value");
            serializer.Serialize(writer, name.FirstName + " " + name.LastName);
            writer.WritePropertyName("src");
            serializer.Serialize(writer, name.Avatar);

            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(User).IsAssignableFrom(objectType);
        }
    }
}