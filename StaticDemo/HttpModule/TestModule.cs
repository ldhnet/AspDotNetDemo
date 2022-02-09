using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HttpModule
{
    public class TestModule : IHttpModule
    {
        public void Dispose() { }
        public void Init(HttpApplication context)
        {
            context.EndRequest += new EventHandler(context_EndRequest);
        }
        void context_EndRequest(object sender, EventArgs e)
        {
            HttpApplication ha = (HttpApplication)sender; 
            string path = ha.Context.Request.Url.ToString().ToLower();

            if (!path.Contains("login")) //是否是登录页面，不是登录页面的话则进入{}
            {
                ha.Response.Write("<!--这是每个页面都会动态生成的文字。--grayworm-->");
            }
            else
            {
                ha.Context.Response.Redirect("/Home/Index");
            }

            Console.WriteLine("TestModule访问次数！");
        }
    }
}
