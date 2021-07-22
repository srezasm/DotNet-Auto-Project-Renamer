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

            WriteLine("well, which one do you want to rename?");

            var allFiles = new List<FileInfo>();
            allFiles.Add(slnFile);
            allFiles.AddRange(csprojFiles);

            for(var i = 1; i < allFiles.Count; i++) {
               WriteLine($"   {i} = {allFiles[i]?.Name}");
            }

            int renameItem;
            while(true) {
               if(!int.TryParse(ReadLine(), out renameItem)) {
                  WriteLine("input isn't valid");
                  continue;
               }
               if(renameItem < 1 || allFiles.Count < renameItem) {
                  WriteLine("input isn't valid");
                  continue;
               }
               break;
            }

            WriteLine("alright, now please give me the new name");
            var newName = ReadLine();

            switch (allFiles[renameItem].Extension)
            {
               case ".sln":

                  break;
               case ".csproj":

                  break;
            }
         }
      }

      public static void WriteTryAgainOrExit()
      {
         WriteLine("please try again or exit by Ctrl+C");
      }
   }
}
