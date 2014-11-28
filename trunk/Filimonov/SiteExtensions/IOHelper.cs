using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace SiteExtensions
{
    public static class IOHelper
    {
       public static void DeleteFile(string relativePath, string fileName)
        {
           if(fileName==null)
               return;
            string absolutePath = HttpContext.Current.Server.MapPath(relativePath);
            string path = Path.Combine(absolutePath, fileName);
            if (File.Exists(path))
                File.Delete(path);
        }

       public static void DeleteDirectory(string relativePath, string directoryName)
        {
            if (directoryName == null)
                return;
            string absolutePath = HttpContext.Current.Server.MapPath(relativePath);
            string path = Path.Combine(absolutePath, directoryName);
           if (Directory.Exists(path))
               Directory.Delete(path, true);
        }

        public static void DeleteFile(string relativePath, string fileName, string extension)
        {
            if (fileName == null)
                return;
            string absolutePath = HttpContext.Current.Server.MapPath(relativePath);

            if (Path.HasExtension(fileName))
            {
                fileName = Path.GetFileNameWithoutExtension(fileName) +"."+ extension;
            }

            string path = Path.Combine(absolutePath, fileName);
            if (File.Exists(path))
                File.Delete(path);
        }

        public static void DeleteFiles(string relativePath, string[] fileNames)
        {
            foreach (var fileName in fileNames.Where(fileName => fileName != null))
            {
                DeleteFile(relativePath, fileName);
            }
        }


        public static string CreateAbsolutePath(string relativePath, string fileName)
        {
            return Path.Combine(HttpContext.Current.Server.MapPath(relativePath), fileName);
        }

        
        public static string GetUniqueFileName(string relativePath, string clientPath)
        {
            string initialName = Path.GetFileName(clientPath);

            if (initialName == null)
            {
                throw new Exception("Невозможно определить имя файла " + clientPath);
            }

            initialName = Regex.Replace(initialName, @"[^a-zA-Z0-9._]", "");

            string result = initialName;

            string filePath = HttpContext.Current.Server.MapPath(relativePath);

            filePath = Path.Combine(filePath, initialName);

            if (File.Exists(filePath))
            {
                string newFileName = MakeNewFileName(initialName);
                result = GetUniqueFileName(relativePath, newFileName);
            }
            return result;
        }

        public static string GetUploadFileName(string relativePath, string clientPath)
        {
            string initialName = Path.GetFileName(clientPath);

            if (initialName == null)
            {
                throw new Exception("Невозможно определить имя файла " + clientPath);
            }

            initialName = Regex.Replace(initialName, @"[^a-zA-Z0-9._]", "");

            string result = initialName;

            string filePath = HttpContext.Current.Server.MapPath(relativePath);

            filePath = Path.Combine(filePath, initialName);

            if (File.Exists(filePath))
            {
                return null;
            }
            return result;
        }

        private static string MakeNewFileName(string fileName)
        {
            string result = fileName;
            if (Path.HasExtension(fileName))
            {

                string ext = Path.GetExtension(fileName);
                result = Path.GetFileNameWithoutExtension(fileName) + "1" + ext;
            }
            else
                result += "1";
            return result;
        }
    }
}
