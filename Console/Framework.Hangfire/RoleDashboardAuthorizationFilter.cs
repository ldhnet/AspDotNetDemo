using Hangfire.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Hangfire
{
    /// <summary>
    /// Dashboard角色权限过滤器
    /// </summary>
    public class RoleDashboardAuthorizationFilter : IDashboardAuthorizationFilter
    {
        private readonly string[] _roles;

        /// <summary>
        /// 初始化一个<see cref="RoleDashboardAuthorizationFilter"/>类型的新实例
        /// </summary>
        public RoleDashboardAuthorizationFilter(string[] roles)
        {
            _roles = roles;
        }

        public bool Authorize(DashboardContext context)
        { 
            string[] roles = new[] {"","" };
            return _roles.Intersect(roles).Any();
        }
    }
}
