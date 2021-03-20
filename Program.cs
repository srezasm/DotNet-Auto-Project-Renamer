using AutoProjectRenamer.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
//using static System.Console;

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
         bool doWhile = true;

         WriteLine("welcome! please enter your project root path");

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

            if(Directory.Exists(rootPath)) {

               // find solution file
               var slnFile = new DirectoryInfo(rootPath).GetFiles().SingleOrDefault(p => p.Extension == ".sln");

               // check if there where no .sln file
               if(slnFile == null || !slnFile.Exists) {
                  WriteLine("there is no solution file (.sln)");
                  WriteTryAgainOrExit();
                  continue;
               }

               // find all project files
               var csprojFiles = GetAllFilesByExtension(rootPath, ".csproj");

               // if solution was same with any of project files
               if(
                  csprojFiles.Length != 0
                  &&
                  csprojFiles.Any(p =>
                     p.Name.Replace(p.Extension, "") ==
                     slnFile.Name.Replace(slnFile.Extension, ""))
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

                              if(!(IsValidFileName(rootPath, $"{newName}.sln") && IsValidFileName(rootPath, $"{newName}.csproj"))) {
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

                              if(!IsValidFileName(rootPath, $"{newName}.sln")) {
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

                        default:
                           WriteLine($"{answer} is not an option :/\ntry again");
                           continue;
                     }
                  }
               }
               else {

               }

               continue;
            }
            else {
               WriteLine("directory doesn't exist");
               WriteTryAgainOrExit();
               continue;
            }
         }
      }

      public static void WriteTryAgainOrExit()
      {
         WriteLine("please try again or exit by Ctrl+C");
      }

      public static FileInfo[] GetAllFilesByExtension(string rootPath, string extension)
      {
         var files = new List<FileInfo>();
         var directories = new DirectoryInfo(rootPath).GetDirectories("*",
            new EnumerationOptions {
               AttributesToSkip = FileAttributes.Hidden
            });

         foreach(var directory in directories) {
            files.AddRange(directory.GetFiles().Where(f => f.Extension == extension));

            if(Directory.GetDirectories(directory.FullName).Any())
               GetAllFilesByExtension(directory.FullName, extension);
         }

         files.AddRange(new DirectoryInfo(rootPath).GetFiles().Where(f => f.Extension == extension));
         return files.ToArray();
      }

      public static bool IsValidFileName(string rootPath, string filename)
      {
         return
            filename.IndexOfAny(Path.GetInvalidFileNameChars()) == -1
            &&
            !File.Exists(Path.Combine(rootPath, filename));
      }
   }
}
