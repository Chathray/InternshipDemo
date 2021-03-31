using Newtonsoft.Json;
using System;

namespace Internship.Application
{
    [JsonConverter(typeof(WhitelistSerializer))]
    public class InternModel
    {
        public string Avatar { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Duration { get; set; }
        public string Type { get; set; }
        public int DepartmentId { get; set; }
        public int OrganizationId { get; set; }
        public int TrainingId { get; set; }

        public int Mentor { get; set; }
        public int InternId { get; set; }
    }

    public class WhitelistSerializer : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var name = value as InternModel;
            writer.WriteStartObject();

            writer.WritePropertyName("iid");
            serializer.Serialize(writer, name.InternId);
            writer.WritePropertyName("src");
            serializer.Serialize(writer, name.Avatar);
            writer.WritePropertyName("value");
            serializer.Serialize(writer, name.FirstName + " " + name.LastName);

            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(InternModel).IsAssignableFrom(objectType);
        }
    }
}