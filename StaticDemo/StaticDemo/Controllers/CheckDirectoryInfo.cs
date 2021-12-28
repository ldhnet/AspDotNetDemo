using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace StaticDemo.Controllers
{
    public class CheckDirectoryInfo
    {
        private const string endPath = "index.html";
        private bool beFind = false;
        private bool beFindSub = false;
        /// <summary>
        /// 递归遍历文件夹
        /// </summary>
        /// <param name="currentFolder"></param>
        /// <returns></returns>
        public bool GetFileFromFileLocation(string currentFolder)
        {
            try
            { 
                string[] currentFiles = Directory.GetFiles(currentFolder);
                if (CheckFileType(currentFiles))
                {
                    beFind = true;
                }
                else
                {
                    foreach (string folder in Directory.GetDirectories(currentFolder))
                    {
                        if (beFind)
                            break;
                        var files = Directory.GetFiles(folder);
                        if (CheckFileType(files))
                        {
                            beFind = true;
                            break;
                        }
                        string[] folders = Directory.GetDirectories(folder);
                        if (folders.Length != 0 && !beFind)
                        {
                            beFind = GetFileFromFileLocation(folder);
                        }
                    }
                }
            }
            catch //(Exception ex)
            {
                beFind = false;
            }
            return beFind;
        }

        public bool CheckSubDirectoryInfo(string currentFolder)
        {
            try
            {
                foreach (string folder in Directory.GetDirectories(currentFolder))
                {
                    if (beFindSub)
                        break;
                    var files = Directory.GetFiles(folder);
                    if (CheckFileType(files))
                    {
                        beFindSub = true;
                        break;
                    }
                    string[] folders = Directory.GetDirectories(folder);
                    if (folders.Length != 0 && !beFindSub)
                    {
                        beFindSub = CheckSubDirectoryInfo(folder);
                    }
                }
            }
            catch //(Exception ex)
            {
                beFindSub = false;
            } 
            return beFindSub;
        }

        private bool CheckFileType(string[] files)
        {
            return files.Any(c => c.ToLower().EndsWith(endPath));
        }
    }
}