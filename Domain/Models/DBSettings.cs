using Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class DynamicDocumentDBSettings : IDynamicDocumentDBSettings
    {
        public string DynamicDocumentCollectionName { get; set; }=String.Empty;
        public string DynamicDocumentConnectionString { get; set; } = String.Empty;
        public string DynamicDocumentDatabaseName { get; set; } = String.Empty;
    }
}
