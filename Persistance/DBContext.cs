using Domain.IRepository;
using Domain.Models;
using MongoDB.Driver;

namespace Persistance
{
    public class DBContext: IDBContext
    {
        private readonly IMongoCollection<TemplateCollection> _template;
        public DBContext(IDynamicDocumentDBSettings dynamicDocumentDBSettings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(dynamicDocumentDBSettings.DynamicDocumentDatabaseName);
            _template = database.GetCollection<TemplateCollection>(dynamicDocumentDBSettings.DynamicDocumentCollectionName);
        }
        public TemplateCollection GetTemplateByType(string templateType)
        {
            return _template.Find(x=>x.TemplateType==templateType).FirstOrDefault();
        }
        public string SaveTemplate(TemplateCollection template)
        {
            _template.InsertOne(template);
            return template.Id!;
        }
    }
}
