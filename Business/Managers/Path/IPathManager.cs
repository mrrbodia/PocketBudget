using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Managers.Path
{
    public interface IPathManager
    {
        PathModel GetPathModel(string id);

        IList<PathModel> GetPathModels();

        PathModel GetDefaultPathModel();
    }
}
