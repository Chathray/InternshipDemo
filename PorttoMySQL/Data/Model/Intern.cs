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

        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public string UpdatedDate { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public string CreatedDate { get; set; }
        public string Duration { get; set; }
        public string Type { get; set; }
        public int DepartmentId { get; set; }
        public int OrganizationId { get; set; }
        public int TrainingId { get; set; }

        [ForeignKey("OrganizationId")]
        public Organization Organizations { get; set; }

        [ForeignKey("DepartmentId")]
        public Department Departments { get; set; }

        [ForeignKey("Mentor")]
        public User Users { get; set; }

        [ForeignKey("TrainingId")]
        public Training Trainings { get; set; }
    }

    public class WhitelistSerializer : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var name = value as Intern;
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
            return typeof(User).IsAssignableFrom(objectType);
        }
    }
}