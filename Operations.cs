using System;
using System.IO;
using System.Linq;
using AutoProjectRenamer.Extensions;
using AutoProjectRenamer.Utils;

namespace AutoProjectRenamer
{
   public class Operations
   {
      public bool ChangeSolutionName(ref FileInfo sln, string newName)
      {
         // change sln base dir name
         // change sln filename
         // change sln content

         // change base dir name
         var baseDir = sln.DirectoryName.Split(Path.DirectorySeparatorChar).ToArray();
         var newDirName = baseDir.Take(baseDir.Length - 1).Append(newName).ToArray();

         if(!FileUtility.IsValidDirectoryName(baseDir.JoinPath(), newDirName.JoinPath()))
            return false;

         Directory.Move(baseDir.JoinPath(), newDirName.JoinPath());
         Program.WriteLine("   base base directory's name changed", ConsoleColor.DarkBlue);


         // change sln filename
         var slnNewName = Path.Combine(newDirName.JoinPath(), newName + ".sln");
         var slnOldName = Path.Combine(newDirName.JoinPath(), sln.Name);
         if(!FileUtility.IsValidFileName(slnOldName, slnNewName))
            return false;
         
         File.Move(slnOldName, slnNewName);
         Program.WriteLine("   sln's name changed", ConsoleColor.DarkBlue);

         return true;
      }

      public void ChangeProjectName(FileInfo csproj, string newName)
      {
         // change project filename
         // change project content to new name
         // change project name in sln
         // change all namespaces
      }

      private bool ValidateDirectoryName()
      {
         return true;
      }

      private void ChangeDirectoryName()
      {

      }
   }
}
