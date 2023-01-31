using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    [BsonIgnoreExtraElements]
    public class TemplateCollection
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("type")]
        public string? TemplateType { get; set; }
        [BsonElement("template")]
        public string? Template { get; set; }
        [BsonElement("templateVariable")]
        public string[]? TemplateVariable { get; set; }
        [BsonElement("isWatermarkImage")]
        public bool IsWatermarkImage { get; set; }
        [BsonElement("watermark")]
        public string? TemplateWatermark { get; set; }
        [BsonElement("watermarkOpacity")]
        public int WatermarkOpacity { get; set; }
    }
}
