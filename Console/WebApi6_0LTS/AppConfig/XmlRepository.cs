using Microsoft.AspNetCore.DataProtection.Repositories;
using System.Xml.Linq;

namespace WebApi6_0.AppConfig
{
    public class XmlRepository : IXmlRepository
    {
        private readonly string _KeyContentPath = "";

        public XmlRepository()
        {
            _KeyContentPath = Path.Combine(Directory.GetCurrentDirectory(), "your file floder", "your key name.xml");
        }

        public IReadOnlyCollection<XElement> GetAllElements()
        {
            //加载key信息
            var elements = new List<XElement>() { XElement.Load(_KeyContentPath) };
            return elements;
        }

        public void StoreElement(XElement element, string friendlyName)
        {
            //本文忽略实现存储功能，因为我们只需要读取已存在的Key即可
        }
    }
}
