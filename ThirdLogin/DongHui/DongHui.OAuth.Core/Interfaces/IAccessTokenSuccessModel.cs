using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DongHui.OAuth.Core.Interfaces
{
	public interface IAccessTokenSuccessModel
	{
		string TokenType { get; set; }

		string AccessToken { get; set; }

		string RefreshToken { get; set; }

		string Scope { get; set; }

		dynamic ExpiresIn { get; set; }
	}
}
