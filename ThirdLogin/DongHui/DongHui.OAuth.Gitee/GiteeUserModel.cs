﻿using DongHui.OAuth.Core.Interfaces;
using System.Text.Json.Serialization;

namespace DongHui.OAuth.Gitee
{  
    public class GiteeUserModel : IUserInfoModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("avatar_url")]
        public string Avatar { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("blog")]
        public string Blog { get; set; }

        [JsonPropertyName("bio")]
        public string Bio { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreateAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdateAt { get; set; }

        [JsonPropertyName("public_repos")]
        public int PublicRepos { get; set; }

        [JsonPropertyName("public_gists")]
        public int PublicGists { get; set; }

        [JsonPropertyName("followers")]
        public int Followers { get; set; }

        [JsonPropertyName("following")]
        public int Following { get; set; }

        [JsonPropertyName("message")]
        public string ErrorMessage { get; set; }
    }
}
