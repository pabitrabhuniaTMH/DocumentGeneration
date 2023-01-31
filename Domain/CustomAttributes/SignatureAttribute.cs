using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CustomAttributes
{
    public class SignatureAttribute:ValidationAttribute
    {
       
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var instance = validationContext.ObjectInstance;
            var proprtyValue = instance.GetType().GetProperty("IsSignature")!.GetValue(instance, null);
            if (proprtyValue != null && (bool)proprtyValue)
            {
                if (value != null && value.ToString() != "")
                    return ValidationResult.Success;
                else
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
            return ValidationResult.Success;
        }
    }
}
