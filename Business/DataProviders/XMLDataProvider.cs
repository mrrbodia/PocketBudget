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
using Business.Xml.Parsers;

namespace Business.DataProviders
{
    public class XMLDataProvider
    {
        private PathXmlParser Parser = new PathXmlParser();

        private const string examplesPath = "/content/xml/PathModels.xml";
        private const string defaultModelPath = "/content/xml/DefaultPathModel.xml";

        private XDocument LoadDocument(string filePath)
        {
            if (!File.Exists(HttpContext.Current.Server.MapPath(filePath)))
                return null;

           return XDocument.Load(@HttpContext.Current.Server.MapPath(filePath));
        }

        public IList<PathModel> GetPathModels()
        {
            var xml = LoadDocument(examplesPath);
                
            return xml.Elements("PathModels").Elements("PathModel")
                .Select(element => Parser.ParsePath(element))
                .ToList();
        }

        public PathModel GetDefaultPathModel()
        {
            var xml = LoadDocument(defaultModelPath);
            return Parser.ParsePath(xml.Element("PathModel"));
        }
    }
}
