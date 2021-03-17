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
        public int InternId { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public int Mentor { get; set; }
        // This foreign key not be null in model, but can null in DB
        // Exception: 'Data is Null. This method or property cannot be called on Null values.'
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string CreatedDate { get; set; }
        public string Duration { get; set; }
        public string Type { get; set; }
        public int Department { get; set; }
        public int Organization { get; set; }

        [ForeignKey("Organization")]
        public Organization Or { get; set; }

        [ForeignKey("Department")]
        public Department De { get; set; }

        [ForeignKey("Mentor")]
        public User Us { get; set; }
    }

    public class WhitelistSerializer : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var name = value as Intern;
            writer.WriteStartObject();

            writer.WritePropertyName("internId");
            serializer.Serialize(writer, name.InternId);
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