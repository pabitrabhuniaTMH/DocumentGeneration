using Domain.Common;
using Domain.IRepository;
using Domain.Models;
using IronPdf;
using IronPdf.Editing;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SixLabors.ImageSharp;
using Spire.Pdf.Security;
using System.Net;

namespace Application.DocumentGenerationServices
{
    public class Generate : IGenerate
    {
        private readonly ITemplate _template;
        private readonly string? _licenseKey;
        private readonly string? _certificate;
        private readonly string? _password;
        public Generate(ITemplate template,IConfiguration configuration)
        {
            _template = template;
            _licenseKey = configuration.GetSection("Document:LicenseKey").Value;
            _certificate = configuration.GetSection("Document:Certificate").Value;
            _password = configuration.GetSection("Document:Password").Value;
        }
        public ApiResponse GeneratePdf(DocumentGenerateModels models)
        {
            try
            {
                var placeholder = JsonConvert.DeserializeObject<Dictionary<string, object>>(models.TemplateVariableValue!)!;
                var templateDbValue = _template.GetTemplateByType(models.TemplateType!);

                var templateVariable = templateDbValue.TemplateVariable;
                var a = ConvertStringToObjectVariable(templateVariable!);
                string templateBody = Aes256.Decrypt(templateDbValue.Template!.ToString());
                foreach(KeyValuePair<string, object> value in placeholder!)
                {
                    if (a.Any(x => x.Variable == value.Key))
                        templateBody = templateBody.Replace(value.Key, value.Value.ToString());
                    else
                        throw new ArgumentException(value.Key+" is a wrong variable name");
                }
                IronPdf.License.LicenseKey = _licenseKey;
                // Instantiate Renderer
                var renderer = new ChromePdfRenderer();              
                // Use sufficient MarginBottom to ensure that the HtmlFooter does not overlap with the main PDF page content.
                renderer.RenderingOptions.MarginBottom = 25;
                // Create a PDF from a HTML
                var pdf = renderer.RenderHtmlAsPdf(templateBody);
                //Metadata
                pdf.MetaData.Title = models.Metadata!.Title;
                pdf.MetaData.Subject=models.Metadata.Subject;
                pdf.MetaData.CreationDate = models.Metadata.CreatoinDate;
                pdf.MetaData.Author=models.Metadata.Auther;
                pdf.MetaData.Creator = models.Metadata.Creator;
                pdf.MetaData.ModifiedDate=models.Metadata.UpdateDate;
                //Add watermark
                if (models.IsWatermark)
                {
                    var tmpWatermark = models.Watermark != null && models.Watermark != "" ? models.Watermark : templateDbValue.TemplateWatermark;
                    var tmpWatermarkOpc = models.WatermarkOpacity > 0 ? models.WatermarkOpacity : templateDbValue.WatermarkOpacity;

                    if (templateDbValue.IsWatermarkImage && File.Exists(tmpWatermark))
                    {
                        // Set Logo as a Watermark with HtmlStamper
                        var backgroundStamp = new HtmlStamper("<img src='"+ tmpWatermark + "'/>")
                        {
                            Opacity = tmpWatermarkOpc,
                            VerticalAlignment = VerticalAlignment.Middle,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            IsStampBehindContent = true,
                        };
                        pdf.ApplyStamp(backgroundStamp);
                    }
                    else
                    {
                        
                        pdf.ApplyWatermark(tmpWatermark, 50, tmpWatermarkOpc,
                           IronPdf.Editing.VerticalAlignment.Middle, IronPdf.Editing.HorizontalAlignment.Center);
                    }
                    
                }
                //Password to open the pdf
                if (models.IsPassword)
                    pdf.SecuritySettings.UserPassword=models.Password;

                Spire.Pdf.PdfDocument doc = new ();
                doc.LoadFromBytes(pdf.BinaryData,pdf.Password);

                if (models.IsSignature)
                {
                    //Load the certificate
                    PdfCertificate cert = new (_certificate, _password);

                    Spire.Pdf.Interactive.DigitalSignatures.PdfOrdinarySignatureMaker signatureMaker = new (doc, cert);
                    signatureMaker.SetAcro6Layers(false);

                    Spire.Pdf.Interactive.DigitalSignatures.PdfSignature signature = signatureMaker.Signature;
                    signature.Name = models.Signature!.Name;
                    signature.Reason = models.Signature.Reason;
                    signature.Location = models.Signature.Location;
                    signature.ContactInfo = models.Signature.ContactInfo;

                    Spire.Pdf.Interactive.DigitalSignatures.PdfSignatureAppearance appearance = new (signature)
                    {
                        NameLabel = models.Signature!.NameLabel,
                        DateLabel = "Date: ",
                        ReasonLabel = "Reason: ",
                        LocationLabel = "Location: ",
                        ContactInfoLabel = "Contact: ",
                        GraphicMode = Spire.Pdf.Interactive.DigitalSignatures.GraphicMode.SignDetail
                    };
                    signatureMaker.MakeSignature("signName", doc.Pages[^1], 50, 725, 200, 70, appearance);
                }               
                doc.SaveToFile(@"C:\Users\Pabitra Bhunia\Desktop\PDF Generate\output.pdf");
                return new ApiResponse
                {
                    Id=TimeStamp.GetTimeStamp(),StatusCode=HttpStatusCode.OK,Status="Success",Message="Document has been successfully saved"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Id = TimeStamp.GetTimeStamp(),
                    StatusCode = HttpStatusCode.InternalServerError,
                    Status = "Failed",
                    Message = ex.Message
                };          
            }
            
        }
        public List<TemplateVariable> ConvertStringToObjectVariable(string[] backendVariable)
        {
            List<TemplateVariable> lst = new ();
            foreach (string str in backendVariable)
            {
                var variable = JsonConvert.DeserializeObject<TemplateVariable>(str);
                lst.Add(variable!);         
            }
            return lst;
        }


    }
}
            