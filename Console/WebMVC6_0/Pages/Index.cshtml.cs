using Framework.Cache;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceStack.Redis;

namespace WebMVC6_0.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger; 

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        { 
            try
			{
                var list = director("C:/RegionalRoot/Applications/Mockup");
                ViewData["list"] = list; 
            }
			catch
			{ 
                ViewData["list"] = new List<string>();
            } 
        }
         
        private List<string> director(string dirs)
        {
            List<string> list = new List<string>();
            //绑定到指定的文件夹目录
            DirectoryInfo dir = new DirectoryInfo(dirs);
            //检索表示当前目录的文件和子目录
            FileSystemInfo[] fsinfos = dir.GetFileSystemInfos();
            //遍历检索的文件和子目录
            foreach (FileSystemInfo fsinfo in fsinfos)
            {
                //判断是否为空文件夹　　
                if (fsinfo is DirectoryInfo)
                {  
                    //递归调用
                    list.Add(fsinfo.FullName.Substring(36) + "/index.html");
                }
                else
                {
                    Console.WriteLine(fsinfo.FullName);
                    //将得到的文件全路径放入到集合中
                    //list.Add(fsinfo.FullName);
                }
            }
            return list;
        }


    }
}