using DongHui.OAuth.Core.Interfaces;
using System.Text.Json.Serialization;

namespace DongHui.OAuth.Gitee
{ 
	public class GiteeAccessTokenModel : IAccessTokenModel
	{
		[JsonPropertyName("token_type")]
		public virtual string TokenType { get; set; }

		[JsonPropertyName("access_token")]
		public virtual string AccessToken { get; set; }

		[JsonPropertyName("refresh_token")]
		public virtual string RefreshToken { get; set; }

		[JsonPropertyName("scope")]
		public virtual string Scope { get; set; }

		[JsonPropertyName("expires_in")]
		public virtual dynamic ExpiresIn { get; set; }

		[JsonPropertyName("error_description")]
		public virtual string ErrorDescription { get; set; }
	}
}
