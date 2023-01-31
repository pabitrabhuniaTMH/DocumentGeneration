using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepository
{
    public interface IDynamicDocumentDBSettings
    {
        public string DynamicDocumentCollectionName { get; set; }
        public string DynamicDocumentConnectionString { get; set; }
        public string DynamicDocumentDatabaseName { get; set; }
    }
}
