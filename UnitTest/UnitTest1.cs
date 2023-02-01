using DocumentGeneration.Controllers;
using Domain.IRepository;
using Domain.Models;
using Moq;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace UnitTest
{
    public class Tests
    { 
        [Test]
        public void Test1()
        {
            Mock<ITemplate> mock = new();
            mock.Setup(x=>x.SaveTemplate(It.IsAny<TemplateModel>())).Returns("");
            var temController = new TemplateController(mock.Object);
            var input = new TemplateModel 
            {
               TemplateType= "Offer Letter",
               Template= "C:\\Users\\Pabitra Bhunia\\Desktop\\PDF Generate\\index.html",
               TemplateVariable = {},
               TemplateWatermark ="ABC",
               IsWatermarkImage=true,
               WatermarkOpacity=30

            };
            var retult = temController.SaveTemplate(input);
            Assert.Pass();
        }
    }
}