using Business.DataProviders;
using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Managers.Path
{
    public class PathManager : IPathManager
    {
        public XMLDataProvider Provider = new XMLDataProvider();

        public IList<PathModel> GetPathModels()
        {
            return Provider.GetPathModels();
        }
    }
}
