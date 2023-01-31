using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepository
{
    public interface ITemplate
    {
        public object SaveTemplate(TemplateModel template);
        public TemplateCollection GetTemplateByType(string templateType);
    }
}
