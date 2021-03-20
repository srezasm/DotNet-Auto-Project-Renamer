using System;
using System.IO;
using System.Linq;
using System.Management.Automation;
using AutoProjectRenamer.Contracts;

namespace AutoProjectRenamer.Extensions
{
   class ChangingName : IChangingName
   {
      /// <summary>
      /// this method changes solution name
      /// </summary>
      /// <param name="fileInfo">This parameter gets FileInfo object to extract all needed information from it</param>
      /// <param name="newName">This parameter specifies the new name for solution file</param>
      /// <returns>if can change , return true </returns>
      public bool ChangeSolutionName(FileInfo fileInfo, string newName)
      {
         try {
            var path = fileInfo.FullName;
            var oldName = Path.GetFileName(path);

            var slnFile = Directory.GetFiles(path).First(c => Path.GetExtension(path) == ".sln");
            var fileName = path.Split('\\').Last();

            var directory = string.Join('\\', path.Split('\\'));
            var splitCount = directory.Split('\\').Count() - 1;
            var rootDirectory = string.Join('\\', directory.Split('\\').Take(splitCount));

            using(PowerShell powerShell = PowerShell.Create()) {
               powerShell.AddScript($"cd {directory}");
               powerShell.AddScript($"rename-item {oldName}.sln {newName}.sln");
               var results = powerShell.Invoke();
            }

            Directory.Move(directory, rootDirectory + "\\" + newName);

            return true;
         }
         catch(Exception) {

            return false;
         }
      }
   }
}
