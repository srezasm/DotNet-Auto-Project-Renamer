using System.IO;
using System.Linq;

namespace AutoProjectRenamer.Extensions
{
   public static class PathExtensions
   {
      public static string JoinPath(this string[] pathArr)
      {
         pathArr = pathArr.Where(p => !string.IsNullOrEmpty(p)).ToArray();
         return string.Join(Path.DirectorySeparatorChar, pathArr);
      }

   }
}
