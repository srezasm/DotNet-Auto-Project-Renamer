using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoProjectRenamer.Contracts
{
    interface IChangingName
    {
        bool ChangeSolutionName(string path,string oldName,string newName);

    }
}
