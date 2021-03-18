using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Console;
namespace AutoProjectRenamer
{
   class Program
   {
      static void Main(string[] args)
      {
         const string baseCommand = "dotnet renamer";
         bool doWhile = true;

         while(doWhile) {
            var rl = ReadLine();

            if(string.IsNullOrEmpty(rl)) {
               continue;
            }
            if(!rl.StartsWith(baseCommand)) {
               WriteLine("this command is not available");
               WriteLine("please use another command or exit by Ctrl+C");
               continue;
            }
            if(rl == $"{baseCommand} --help") {
               //todo: write a help message for all available commands
               WriteLine("Hello");
               continue;
            }

            //warn: could be written better.. maybe..
            var rootPath = Regex.Match(rl, @""".*""$").ToString().Replace(@"""", "").Trim().TrimEnd('\\');
            if(Directory.Exists(rootPath)) {
               // check if there where no .sln file
               if(new DirectoryInfo(rootPath).GetFiles().All(p => p.Extension != ".sln")) {
                  WriteLine("there is no solution file (.sln)");
                  WriteLine("please try again or exit by Ctrl+C");
                  continue;
               }

               // get solution and project files by their extension
               var slnFile = new DirectoryInfo(rootPath).GetFiles().SingleOrDefault(p => p.Extension == ".sln");
               //bug: there could be more than one project file in the root path (►__◄)
               var csprojFile = new DirectoryInfo(rootPath).GetFiles().SingleOrDefault(f => f.Extension == ".csproj");
               
               // if .csproj and .sln files have a same name
               if(
                  csprojFile != null
                  &&
                  // if .csproj and .sln files are in a same directory and have a same name
                  slnFile?.Name.Replace(slnFile.Extension, "") == csprojFile.Name.Replace(csprojFile.Extension, "")
                  ||
                  //todo: check if there really was a .csproj file into directory
                  // if .csproj and .sln files are not in a same directory but have a same name
                  new DirectoryInfo(rootPath).GetDirectories().Any(p => p.Name == slnFile?.Name.Replace(slnFile.Extension, ""))
               ) {
                  WriteLine("your solution (.sln) and project (.csproj) file have a same name");
                  WriteLine("do you want to change the both file names? (y/n)");
                  WriteLine("[y] means both");
                  WriteLine("[n] means just solution");

                  var answer = ReadLine();
                  string newName;

                  if(string.IsNullOrEmpty(answer))
                     continue;

                  switch(answer?.Trim()) {
                     case "y":
                        while(true) {
                           WriteLine("alright, now what's the new name?");
                           newName = ReadLine();

                           if(!(IsValidFileName(rootPath, $"{newName}.sln") && IsValidFileName(rootPath, $"{newName}.csproj"))) {
                              WriteLine("entered name is invalid");
                              WriteLine("HINT: maybe invalid characters or duplicated name");
                              WriteLine("please try again or exit by Ctrl+C");
                           }
                        }

                        ///////////////////////////////////////////
                        // KEEP CALM NIGGA THIS IS THE HARD PART //
                        ///////////////////////////////////////////
                        // CHANGE FILES WITH NEW NAME //
                        ////////////////////////////////

                        break;

                     case "n":
                        while(true) {
                           WriteLine("alright, now what's the new name?");
                           newName = ReadLine();

                           if(!IsValidFileName(rootPath, $"{newName}.sln")) {
                              WriteLine("entered name is invalid");
                              WriteLine("HINT: maybe invalid characters or duplicated name");
                              WriteLine("please try again or exit by Ctrl+C");
                           }
                        }

                        ///////////////////////////////////////////
                        // KEEP CALM NIGGA THIS IS THE HARD PART //
                        ///////////////////////////////////////////
                        // CHANGE FILES WITH NEW NAME //
                        ////////////////////////////////

                        break;

                     default:
                        continue;
                  }
               }
               // if 
               else {

               }

               continue;
            }
            else {
               WriteLine("directory doesn't exist, please try again or exit by Ctrl+C");
               continue;
            }

         }
      }

      public static bool IsValidFileName(string rootPath, string filename)
      {
         bool isValid;
         isValid = filename.IndexOfAny(Path.GetInvalidFileNameChars()) == 0 &&
                   File.Exists(Path.Combine(rootPath, filename));
         return isValid;
      }
   }
}
