using AutoProjectRenamer.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace AutoProjectRenamer.Models
{
    class ChangingName : IChangingName
    {
        /// <summary>
        /// this method, change your solution to new name.
        /// 
        /// </summary>
        /// <param name="path">should hame .sln file in path </param>
        /// <param name="oldName">original name for solution</param>
        /// <param name="newName">new name for solution</param>
        /// <returns>if can change , return true </returns>
        public bool ChangeSolutionName(string path, string oldName, string newName)
        {
            try
            {
                var slnFile = Directory.GetFiles(path).First(c => string.Concat(c.TakeLast(4)) == ".sln");
                var fileName = path.Split('\\').Last();
                var directory = string.Join('\\', path.Split('\\'));
                var splitCount = directory.Split('\\').Count() - 1;
                var rootDirectory = string.Join('\\', directory.Split('\\').Take(splitCount));
                using (PowerShell powerShell = PowerShell.Create())
                {
                    powerShell.AddScript($"cd {directory}");
                    powerShell.AddScript($"rename-item {oldName}.sln {newName}.sln");
                    powerShell.AddScript("git init");
                    powerShell.AddScript("git add .");
                    powerShell.AddScript($"git commit -m \"this commit from AutoProjectRenamer for Rename your Solution to {newName}\" ");
                    Collection<PSObject> results = powerShell.Invoke();
                }

                Directory.Move(directory, rootDirectory + "\\" + newName);

                return true;
            }
            catch (Exception)
            {

                return false;
            }
            
        }
    }
}
