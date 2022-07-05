using DongHui.OAuth.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DongHui.OAuth.Core.Extensions
{
	public static class AccessTokenModelModelExtensions
	{
		public static bool HasError(this IAccessTokenModel accessTokenModel)
		{
			if (accessTokenModel != null && !string.IsNullOrEmpty(accessTokenModel.AccessToken))
			{
				return !string.IsNullOrEmpty(accessTokenModel.ErrorDescription);
			}
			return true;
		}

	}
}
