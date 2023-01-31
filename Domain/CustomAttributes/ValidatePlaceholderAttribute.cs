using Domain.IRepository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CustomAttributes
{
    public class ValidatePlaceholderAttribute:ValidationAttribute
    {
        public ValidatePlaceholderAttribute(ITemplate template)
        {

        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var instance = validationContext.ObjectInstance;
            var proprtyvalue = instance.GetType().GetProperty("TemplateType")!.GetValue(instance, null);
            if (value!=null && value.ToString()!="")
            {
                var placeholder = JsonConvert.DeserializeObject<Dictionary<string, object>>(value!.ToString()!);
                
                string[] htmlString = File.ReadAllText(@"C:\\Users\\Pabitra Bhunia\\Desktop\\PDF Generate\\placeholder.txt").Split(',');
                if (htmlString.Length > 0 && placeholder != null && placeholder.Keys.Count == htmlString.Length)
                {
                    foreach (KeyValuePair<string, object> val in placeholder)
                    {
                        if (!htmlString.Any(x => x == val.Key))
                        {
                            return new ValidationResult(val.Key + " This placeholder is not matched with the backend placeholder");
                        }
                        
                    }
                    return ValidationResult.Success;
                }
            }
            
            return new ValidationResult("Placeholder is not matched with the backend placeholder");
        }

    }
}
