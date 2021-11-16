using Microsoft.VisualBasic.FileIO;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace Framework.Utility.Helper
{
    /// <summary>
    /// 文件辅助操作类
    /// </summary>
    public class FileToolHelper
    {
        /// <summary>
        /// 删除文件（到回收站[可选]）
        /// </summary>
        /// <param name="filename">要删除的文件名</param>
        /// <param name="isSendToRecycleBin">是否删除到回收站</param>
        public static void Delete(string filename, bool isSendToRecycleBin = false)
        {
            if (isSendToRecycleBin)
            {
                FileSystem.DeleteFile(filename, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
            }
            else
            {
                File.Delete(filename);
            }
        }

        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="strDirectory"></param>
        public static void DeleteFolderFile(string strDirectory)
        {
            var fileList = GetAllFilesInDir(strDirectory);
            if (fileList.Count > 0)
            {
                foreach (var item in fileList)
                {
                    Delete(item.FullName, true);
                }
            }

            Directory.Delete(strDirectory, true);
        }

        /// <summary>
        /// 返回指定目录下的所有文件信息
        /// </summary>
        /// <param name="strDirectory"></param>
        /// <returns></returns>
        public static List<FileInfo> GetAllFilesInDir(string strDirectory)
        {
            List<FileInfo> listFiles = new List<FileInfo>(); //保存所有的文件信息
            DirectoryInfo directory = new DirectoryInfo(strDirectory);
            DirectoryInfo[] directoryArray = directory.GetDirectories();
            FileInfo[] fileInfoArray = directory.GetFiles();
            if (fileInfoArray.Length > 0) listFiles.AddRange(fileInfoArray);
            foreach (DirectoryInfo _directoryInfo in directoryArray)
            {
                DirectoryInfo directoryA = new DirectoryInfo(_directoryInfo.FullName);
                DirectoryInfo[] directoryArrayA = directoryA.GetDirectories();
                FileInfo[] fileInfoArrayA = directoryA.GetFiles();
                if (fileInfoArrayA.Length > 0) listFiles.AddRange(fileInfoArrayA);
                GetAllFilesInDir(_directoryInfo.FullName);//递归遍历
            }
            return listFiles;
        }

        /// <summary>
        /// 设置或取消文件的指定<see cref="FileAttributes"/>属性
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="attribute">要设置的文件属性</param>
        /// <param name="isSet">true为设置，false为取消</param>
        public static void SetAttribute(string fileName, FileAttributes attribute, bool isSet)
        {
            FileInfo fi = new FileInfo(fileName);
            if (!fi.Exists)
            {
                throw new FileNotFoundException("要设置属性的文件不存在。", fileName);
            }
            if (isSet)
            {
                fi.Attributes = fi.Attributes | attribute;
            }
            else
            {
                fi.Attributes = fi.Attributes & ~attribute;
            }
        }

        /// <summary>
        /// 获取文件版本号
        /// </summary>
        /// <param name="fileName"> 完整文件名 </param>
        /// <returns> 文件版本号 </returns>
        public static string GetVersion(string fileName)
        {
            if (File.Exists(fileName))
            {
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(fileName);
                return fvi.FileVersion;
            }
            return null;
        }

        /// <summary>
        /// 获取文件的MD5值
        /// </summary>
        /// <param name="fileName"> 文件名 </param>
        /// <returns> 32位MD5 </returns>
        public static string GetFileMd5(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            const int bufferSize = 1024 * 1024;
            byte[] buffer = new byte[bufferSize];

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            md5.Initialize();

            long offset = 0;
            while (offset < fs.Length)
            {
                long readSize = bufferSize;
                if (offset + readSize > fs.Length)
                {
                    readSize = fs.Length - offset;
                }
                fs.Read(buffer, 0, (int)readSize);
                if (offset + readSize < fs.Length)
                {
                    md5.TransformBlock(buffer, 0, (int)readSize, buffer, 0);
                }
                else
                {
                    md5.TransformFinalBlock(buffer, 0, (int)readSize);
                }
                offset += bufferSize;
            }
            fs.Close();
            byte[] result = md5.Hash;
            md5.Clear();
            StringBuilder sb = new StringBuilder(32);
            foreach (byte b in result)
            {
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString();
        }

        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="path"></param>
        ///// <param name="fileName"></param>
        ///// <returns></returns>
        //private static string BuildFullFilePath(string path, string fileName)
        //{
        //    var appPath = GlobalContext.HostingEnvironment.WebRootPath;
        //    var relativePath = path.StartsWith("/") ? path.Substring(1) : path;
        //    var fullPath = Path.Combine(appPath, relativePath);
        //    if (!Directory.Exists(fullPath))
        //    {
        //        Directory.CreateDirectory(fullPath);
        //        //throw new Exception($"未找到文件目录：{path}");
        //    }

        //    return fullPath + Path.AltDirectorySeparatorChar + fileName;
        //}

        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="path"></param>
        ///// <returns></returns>
        //public static void BuildFullFilePath(string path)
        //{
        //    var appPath = GlobalContext.HostingEnvironment.WebRootPath;
        //    var relativePath = path.StartsWith("/") ? path.Substring(1) : path;
        //    var fullPath = Path.Combine(appPath, relativePath);
        //    if (!Directory.Exists(fullPath))
        //    {
        //        Directory.CreateDirectory(fullPath);
        //    }
        //}

        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="path"></param>
        ///// <param name="fileName"></param>
        ///// <param name="deleteOriginal"></param>
        ///// <returns></returns>
        //public static FileInfo GenerateFileInfo(string path, string fileName, bool deleteOriginal = true)
        //{
        //    var fullPath = BuildFullFilePath(path, fileName);

        //    if (File.Exists(fullPath) && deleteOriginal)
        //        File.Delete(fullPath);

        //    var fileInfo = new FileInfo(fullPath);

        //    return fileInfo;
        //}

        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="fileName"></param>
        ///// <param name="deleteOriginal"></param>
        ///// <returns></returns>
        //public static FileInfo GenerateTempFileInfo(string fileName, bool deleteOriginal = true)
        //{
        //    return GenerateFileInfo("Temp", fileName, deleteOriginal);
        //}

        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="fileName"></param>
        ///// <param name="sourcePath"></param>
        ///// <param name="destPath"></param>
        ///// <returns></returns>
        //public static FileInfo CopyFileInfo(string fileName, string sourcePath, string destPath)
        //{
        //    if (string.IsNullOrEmpty(fileName)) return null;

        //    var destFileName = BuildFullFilePath(destPath, fileName);
        //    var source = GenerateFileInfo(fileName, sourcePath, false);
        //    if (source == null) return null;

        //    var newFile = source.CopyTo(destFileName, true);
        //    source.Delete();
        //    return newFile;
        //}

        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="fileName"></param>
        ///// <param name="sourcePath"></param>
        ///// <param name="destPath"></param>
        //public static void Move(string fileName, string sourcePath, string destPath)
        //{
        //    var sourceFileName = BuildFullFilePath(sourcePath, fileName);
        //    var destFileName = BuildFullFilePath(destPath, fileName);
        //    File.Move(sourceFileName, destFileName);
        //}

        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="path"></param>
        //public static void ClearDirectory(string path)
        //{
        //    if (Directory.Exists(path))
        //    {
        //        Directory.Delete(path, true);
        //    }
        //}

        /// <summary>
        ///
        /// </summary>
        /// <param name="tempname"></param>
        /// <returns></returns>
        public static string GetTemp(string tempname)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Template\Notification", tempname);
            return File.Exists(path) ? File.ReadAllText(path, System.Text.Encoding.UTF8) : string.Empty;
        }
    }
}