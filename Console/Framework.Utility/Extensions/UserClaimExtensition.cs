using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Framework.Utility.Extensions
{
    public static class UserClaimExtensition
    {
        public static string GetClaim(this IEnumerable<Claim> claims,string name)
        {
            foreach (var item in claims)
            {
                if (item.Type == name)
                    return item.Value;
            }
            return "";
        }
    }
}
