using Domain.CustomAttributes;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class DocumentGenerateModels
    {
        public string? TemplateName { get; set; }
        public string? TemplateType { get; set; } 
        public string? TemplateVariableValue { get; set; }
        public bool IsWatermark { get; set; }
        public string? Watermark { get; set; }
        public int WatermarkOpacity { get; set; }
        public DocumentMetadata? Metadata { get; set; }
        public bool IsSignature { get; set; }
        public DocumentSignature? Signature { get; set; }
        public bool IsPassword { get; set; }
        [PasswordAttribute]
        public string? Password { get; set; }
    }
    public class DocumentMetadata
    {
        public string? Title { get; set; }
        public string? Subject { get; set; }
        public string? Auther { get; set; }
        public DateTime CreatoinDate { get; set; }
        public string? Creator { get; set; }
        public DateTime UpdateDate { get; set; }

    }
    public class DocumentSignature
    {
        public string? Name { get; set; }
        public string? NameLabel { get; set; }
        public string? Reason { get; set; }
        public string? Location { get; set; }
        public string? ContactInfo { get; set; }
    }

    public class TemplateModel
    {
        [Required(ErrorMessage ="Template type is requierd")]
        public string? TemplateType { get; set; }
        [Required(ErrorMessage = "Template is requierd")]
        public string? Template { get; set; }
        [Required(ErrorMessage = "Template variable is requierd")]
        public string[]? TemplateVariable { get; set; }
        [Required(ErrorMessage = "IsWatermarkImage is requierd")]
        public bool IsWatermarkImage { get; set; }
        public string? TemplateWatermark { get; set; }
        public int WatermarkOpacity { get; set; }

    }
    public class TemplateVariable 
    {
        public string? Variable { get; set; }
        public VariableType type { get; set; }
        public bool isMandatory { get; set; }
        public object? defultValue { get; set; }
    }


    public enum VariableType
    {
        String,
        Integer,
        Float,
        Long
    }
}
