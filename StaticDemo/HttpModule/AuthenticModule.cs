using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HttpModule
{
    public class AuthenticModule : IHttpModule
    {
        public void Dispose() { }
        public void Init(HttpApplication context)
        {
            context.PreRequestHandlerExecute += new EventHandler(context_PreRequestHandlerExecute);
        }
        void context_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            //HttpApplication ha = (HttpApplication)sender;
            //string path = ha.Context.Request.Url.ToString();
            //string n = path.ToLower();
            //if (!n.Contains("Login")) //是否是登录页面，不是登录页面的话则进入{}
            //{
            //    if (ha.Context.Session["user"] == null) //是否Session中有用户名，若是空的话，转向登录页。
            //    {
            //        ha.Context.Response.Redirect("/Home/Login");
            //    }
            //}
            Console.WriteLine("AuthenticModule访问次数！");
        }
    }
}
