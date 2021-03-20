using System.IO;

namespace AutoProjectRenamer.Contracts
{
   interface IChangingName
   {
      bool ChangeSolutionName(FileInfo fileInfo, string newName);
   }
}
