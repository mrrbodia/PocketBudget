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

        public IList<PathModel> GetPathModels(string filePath = examplesPath)
        {
            if (!File.Exists(HttpContext.Current.Server.MapPath(filePath)))
                return new List<PathModel>();

            var xml = XDocument.Load(@HttpContext.Current.Server.MapPath(filePath));
            var elements = xml.Elements("PathModel")
                .Select(element => Parser.ParsePath(element))
                .ToList();

            return elements;
        }

        public PathModel GetDefaultPathModel()
        {
            return GetPathModels(defaultModelPath).FirstOrDefault();
        }
    }
}
