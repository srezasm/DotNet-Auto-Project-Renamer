using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AutoProjectRenamer.Utils
{
   public static class FileUtility
   {
      //public static FileInfo[] GetAllFilesByExtension(string rootPath, string extension)
      //{
      //   var files = new List<FileInfo>();
      //   var directories = new DirectoryInfo(rootPath).GetDirectories("*",
      //      new EnumerationOptions {
      //         AttributesToSkip = FileAttributes.Hidden
      //      });

      //   foreach(var directory in directories) {
      //      files.AddRange(directory.GetFiles().Where(f => f.Extension == extension));

      //      if(Directory.GetDirectories(directory.FullName).Any())
      //         GetAllFilesByExtension(directory.FullName, extension);
      //   }

      //   files.AddRange(new DirectoryInfo(rootPath).GetFiles().Where(f => f.Extension == extension));
      //   return files.ToArray();
      //}

      public static bool IsValidFileName(string rootPath, string filename)
      {
         return
            filename.IndexOfAny(Path.GetInvalidFileNameChars()) == -1
            &&
            !File.Exists(Path.Combine(rootPath, filename));
      }

      public static bool IsValidDirectoryName(string rootPath, string dirName)
      {
         return
            dirName.IndexOfAny(Path.GetInvalidFileNameChars()) == -1
            &&
            !Directory.Exists(Path.Combine(rootPath, dirName));
      }

      private static List<FileInfo> GetAllFilesByExtension(string dirPath, string extension, string[] exceptionDirectories = null)
      {
         List<FileInfo> GetFiles(string path)
         {
            var refResult = new List<FileInfo>();

            var dirName = Path.GetFileName(path);
            if(exceptionDirectories.Any(p => p == dirName))
               return new List<FileInfo>();

            // add nested files in the path that match the extension
            refResult.AddRange(
               new DirectoryInfo(path).GetFiles("*",
                  new EnumerationOptions {
                     AttributesToSkip = FileAttributes.Hidden,
                     IgnoreInaccessible = true
                  })
               .Where(p => p.Extension == extension)
               );

            var nestedDirs = new DirectoryInfo(path).GetDirectories();
            if(nestedDirs.Length != 0) {
               var tasks = new List<Task>();

               foreach(var dir in nestedDirs) {
                  tasks.Add(Task.Run(() =>
                     refResult.AddRange(GetFiles(dir.FullName))
                     ));
               }

               Task.WaitAll(tasks.ToArray());
            }

            return refResult;
         }

         var result = GetFiles(dirPath);
         result.RemoveAll(p => p == null);
         return result;
      }

      public static List<FileInfo> GetAllCsFiles(string path)
      {
         return GetAllFilesByExtension(path, ".cs", new[] { "obg", "bin", "Properties", ".git", ".vs" });
      }

      public static List<FileInfo> GetAllCsprojFiles(string path)
      {
         return GetAllFilesByExtension(path, ".csproj", new[] { "obg", "bin", "Properties", ".git", ".vs" });
      }
   }
}
