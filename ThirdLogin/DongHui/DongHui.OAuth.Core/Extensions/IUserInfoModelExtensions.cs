using DongHui.OAuth.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DongHui.OAuth.Core.Extensions
{
	public static class IUserInfoModelExtensions
	{
		public static bool HasError(this IUserInfoModel userInfoModel)
		{
			if (userInfoModel != null)
			{
				return string.IsNullOrEmpty(userInfoModel.Name);
			}
			return true;
		}
	}
}
