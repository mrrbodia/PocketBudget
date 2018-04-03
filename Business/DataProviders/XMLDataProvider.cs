using Business.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Web;
using System.Xml.Linq;

namespace Business.DataProviders
{
    public class XMLDataProvider
    {
        public IList<PathModel> GetPathModels()
        {
            var filePath = "/content/xml/PathModels.xml";

            if (!File.Exists(HttpContext.Current.Server.MapPath(filePath)))
                return new List<PathModel>();

            var xml = XDocument.Load(@HttpContext.Current.Server.MapPath(filePath));

            return new List<PathModel>();
        }
    }
}
