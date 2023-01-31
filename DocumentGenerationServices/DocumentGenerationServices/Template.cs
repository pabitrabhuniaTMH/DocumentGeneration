using Domain.Common;
using Domain.IRepository;
using Domain.Models;

namespace Application.DocumentGenerationServices
{
    
    public class Template : ITemplate
    {
        private readonly IDBContext _dBContext;
        public Template(IDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public TemplateCollection GetTemplateByType(string templateType)
        {
            return _dBContext.GetTemplateByType(templateType);
        }

        public object SaveTemplate(TemplateModel template)
        {
            if (template.Template is null)
                throw new ArgumentNullException("Tempale should not be null");

            if(File.Exists(template.Template))
                template.Template = File.ReadAllText(template.Template);

            template.Template = Aes256.Encrypt(template.Template!);
            var setEntity = new TemplateCollection
            {
                TemplateType = template.TemplateType,
                Template = template.Template,
                TemplateVariable=template.TemplateVariable,
                TemplateWatermark=template.TemplateWatermark,
                IsWatermarkImage=template.IsWatermarkImage,
                WatermarkOpacity=template.WatermarkOpacity,                
            };
            return _dBContext.SaveTemplate(setEntity);
        }
    }
}
