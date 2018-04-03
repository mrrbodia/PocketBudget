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
        IList<PathModel> GetPathModels();

        PathModel GetDefaultPathModel();
    }
}
