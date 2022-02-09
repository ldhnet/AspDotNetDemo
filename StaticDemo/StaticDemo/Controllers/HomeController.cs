using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StaticDemo.Controllers
{
    public class HomeController : Controller
    {
        private const string watchPath = "C:/vue/";
        private const string endPath = "index.html";
        public ActionResult Login()
        { 
            return View();
        }
        public ActionResult Index(string pathName = "")
        {
            try
            {
                int startlen = 0;
                int endlen = endPath.Length + 1;
                int watchlen = watchPath.Length;
                int filelen = watchPath.Length + pathName.Length;

                var result = GetDirector(Path.Combine(watchPath, pathName));
                 
                var date_1 = result.Item2.Where(h => h.FullName.Length <= (filelen + endlen));
                 
                var date_2 = result.Item2.Except(date_1)
                                        .Where(f => f.FullName.Length > watchlen)
                                        .OrderByDescending(c => c.CreationTime)
                                        .Select(x => x.FullName.Substring(watchlen))
                                        .ToList();

                if (!string.IsNullOrWhiteSpace(pathName))
                {
                    startlen = pathName.Length + 1;
                }

                var list_1 = date_1.Select(key => new ResultFileInfo()
                {
                    DisplayName = key.FullName.Substring(filelen),
                    PathUrl = key.FullName.Substring(filelen),
                }).ToList();

                var list_2 = date_2.Select(key => new ResultFileInfo()
                {
                    DisplayName = key.Substring(startlen, key.Length - startlen - endlen),
                    PathUrl = key
                }).Where(c => c.DisplayName.Length > 6)
                                                  .Select(x => new { x, y = x.DisplayName.Substring(x.DisplayName.Length - 6, 6) })
                                                  .OrderByDescending(o => o.y)
                                                  .Select(r => r.x).ToList();
                var list = new List<ResultFileInfo>();
                list.AddRange(list_2);
                list.AddRange(list_1); 

                ViewData["list"] = list;

                ViewData["filelist"] = result.Item1.Select(c => c.FullName.Substring(watchlen)).OrderBy(key => key).ToList();
            }
            catch
            {
                ViewData["list"] = new List<ResultFileInfo>();
                ViewData["filelist"] = new List<string>();
            }
            return View();
        }
        private Tuple<List<FileSystemInfo>, List<FileInfo>> GetDirector(string dirs)
        {
            Tuple<List<FileSystemInfo>, List<FileInfo>> list = Tuple.Create(new List<FileSystemInfo>(), new List<FileInfo>());
            DirectoryInfo dir = new DirectoryInfo(dirs);
            //检索表示当前目录的文件和子目录
            FileSystemInfo[] fsinfos = dir.GetFileSystemInfos()?.ToArray();    
             
            foreach (FileSystemInfo fsinfo in fsinfos)
            {
                var CheckDirectory = new CheckDirectoryInfo();           
                if (fsinfo is DirectoryInfo)
                {
                    var IsValid = CheckDirectory.GetFileFromFileLocation(fsinfo.FullName);
                    if (IsValid)
                        GetFileInfo(fsinfo, list);
                }
                else
                { 
                    if (fsinfo.Name.ToLower().EndsWith(endPath))
                    { 
                        list.Item2.Add((FileInfo)fsinfo);
                    }
                }                 
            }
            return list;
        }

        private void GetFileInfo(FileSystemInfo fileInfo, Tuple<List<FileSystemInfo>, List<FileInfo>> tuple)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(fileInfo.FullName);
            FileInfo[] files = dirInfo.GetFiles();

            var CheckDirectory = new CheckDirectoryInfo();
            var IsValid = CheckDirectory.CheckSubDirectoryInfo(fileInfo.FullName);

            if (files.Any(c => c.Name.ToLower().EndsWith(endPath)) && !IsValid)
            {
                var info = files.FirstOrDefault(c => c.Name.ToLower().EndsWith(endPath));
                tuple.Item2.Add(info);
            }
            else
            {
                tuple.Item1.Add(fileInfo);
            }
        }
      
        #region
        //private List<string> GetDirector(string dirs, List<string> list)
        //{
        //    DirectoryInfo dir = new DirectoryInfo(dirs);
        //    //检索表示当前目录的文件和子目录
        //    FileSystemInfo[] fsinfos = dir.GetFileSystemInfos()?.ToArray();       
        //    foreach (FileSystemInfo fsinfo in fsinfos)
        //    {
        //        //判断是否为空文件夹　　
        //        if (fsinfo is DirectoryInfo)
        //        {
        //            DirectoryInfo dir3 = new DirectoryInfo(fsinfo.FullName);
        //            if (!GetFileInfo(dir3.GetFiles(), list))
        //            {
        //                GetDirector(fsinfo.FullName, list);
        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine(fsinfo.FullName);
        //        }
        //    }
        //    return list;
        //}

        //private bool GetFileInfo(FileInfo[] files, List<string> list)
        //{
        //    if (files.Any(c => c.Name.EndsWith("index.html")))
        //    {
        //        var name = files.FirstOrDefault(c => c.Name.EndsWith("index.html"))?.FullName?.Substring(36);
        //        list.Add(name);
        //        return true;
        //    }
        //    return false;
        //}
        #endregion
    }

    public class ResultFileInfo
    {
        public string DisplayName { get; set; }
        public string PathUrl { get; set; }
    }
}