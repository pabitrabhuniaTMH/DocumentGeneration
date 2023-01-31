using Domain.Models;

namespace Domain.IRepository
{
    public interface IDBContext
    {
        public string SaveTemplate(TemplateCollection template);
        public TemplateCollection GetTemplateByType(string templateType);
    }
}
