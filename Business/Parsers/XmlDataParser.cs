using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace News.Business.Parsers
{
    public class XmlDataParser<T>
    {
        public static IList<T> ParseList(string xml)
        {
            using (var stringReader = new StringReader(xml))
            {
                var serializer = new XmlSerializer(typeof(List<T>));
                return (IList<T>)serializer.Deserialize(stringReader);
            }
        }
    }
}
