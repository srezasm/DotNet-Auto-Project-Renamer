using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using AutoProjectRenamer.Utils;

namespace AutoProjectRenamer
{
   class Program
   {
      public static void WriteLine(string buffer, ConsoleColor foreground = ConsoleColor.DarkGreen, ConsoleColor backgroundColor = ConsoleColor.Black)
      {
         Console.ForegroundColor = foreground;
         Console.BackgroundColor = backgroundColor;
         Console.WriteLine(buffer);
         Console.ResetColor();
      }

      public static string ReadLine()
      {
         var line = Console.ReadLine();
         return line ?? string.Empty;
      }

      static void Main(string[] args)
      {
         Console.Title = "DotNet Project Renamer";

         WriteLine("welcome! please give me your project path");

         bool doWhile = true;

         while(doWhile) {

            var rl = ReadLine();

            if(string.IsNullOrEmpty(rl)) {
               continue;
            }

            var rootPath = rl.Trim('\"');
            if(!Path.IsPathFullyQualified(rl)) {
               WriteLine("welcome! please enter your project root path");
               continue;
            }

            if(!Directory.Exists(rootPath)) {
               WriteLine("path isn't correct");
               WriteTryAgainOrExit();
               continue;
            }

            // find solution file
            var slnFile = new DirectoryInfo(rootPath).GetFiles().SingleOrDefault(p => p.Extension == ".sln");

            // check if there where no .sln file
            if(slnFile == null || !slnFile.Exists) {
               WriteLine("there is no solution file (.sln)");
               WriteTryAgainOrExit();
               continue;
            }

            // find all project files
            var csprojFiles = FileUtility.GetAllCsprojFiles(rootPath);

            // if solution was same with any of project files
            if(
               csprojFiles.Count != 0
               &&
               csprojFiles.Any(p =>
                  Path.GetFileNameWithoutExtension(p.Name) ==
                  Path.GetFileNameWithoutExtension(slnFile.Name))
            ) {
               WriteLine("your solution (.sln) and project (.csproj) file have a same name");
               WriteLine("do you want to change the both file names? (y/n)");
               WriteLine("[y] means both");
               WriteLine("[n] means just solution");

               while(true) {
                  var answer = ReadLine();
                  string newName;

                  if(string.IsNullOrEmpty(answer))
                     continue;

                  switch(answer?.Trim()) {
                     case "y":
                        while(true) {
                           WriteLine("alright, now what's the new name?");
                           newName = ReadLine();

                           if(!(FileUtility.IsValidFileName(rootPath, $"{newName}.sln") && FileUtility.IsValidFileName(rootPath, $"{newName}.csproj"))) {
                              WriteLine("entered name is invalid");
                              WriteLine("HINT: maybe invalid characters or duplicated name");
                              WriteTryAgainOrExit();
                           }
                           else
                              break;
                        }

                        WriteLine("operation started ;)");
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

                           if(!FileUtility.IsValidFileName(rootPath, $"{newName}.sln")) {
                              WriteLine("entered name is invalid");
                              WriteLine("HINT: maybe invalid characters or duplicated name");
                              WriteTryAgainOrExit();
                           }
                           else
                              break;
                        }

                        WriteLine("operation started ;)");

                        new Operations().ChangeSolutionName(ref slnFile, newName);

                        WriteLine("operation completed ;)");


                        break;

                     default:
                        WriteLine($"{answer} is not an option. try again!");
                        continue;
                  }
               }
            }
            else {
               WriteLine("solution and project file(s) have different name");
               WriteLine("please give me the number of each item that you want to rename");
               var allFiles = csprojFiles.Append(slnFile).ToArray();
               for(var i = 0; i < allFiles.Length; i++) {
                  WriteLine($"   {i} = {allFiles[i]?.Name}");
               }

               while(true) {
                  if(!int.TryParse(ReadLine(), out int renameItem)) {
                     WriteLine("input is not an integer. try again!");
                     continue;
                  }
                  if(renameItem < 0 || allFiles.Length - 1 < renameItem) {
                     WriteLine("input is wrong. try again!");
                     continue;
                  }

                  WriteLine("alright, now please give me the new name");
                  var newName = ReadLine();

               }
            }

            continue;
         }
      }

      public static void WriteTryAgainOrExit()
      {
         WriteLine("please try again or exit by Ctrl+C");
      }
   }
}
