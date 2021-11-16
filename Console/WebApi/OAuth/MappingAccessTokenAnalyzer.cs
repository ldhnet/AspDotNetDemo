using System;

namespace WebApi.OAuth
{
    public class MappingAccessTokenAnalyzer
    {
        public static OAuth_ClientAuthorization GetAccessToken(string accessToken)
        {
            var result = new OAuth_ClientAuthorization()
            {
                AuthorizationId = 1,
                Token = "123",
                Scope = "test",
                UserId = "00001",
                ExpirationDateUtc = DateTime.Now.AddYears(1),
                CreatedOnUtc = DateTime.Now,
            };
            return result;
            //using (var db = new OAuthDbContext())
            //{
            //    var query = from auth in db.ClientAuthorizations
            //                from client in db.Clients
            //                where auth.ClientId == client.ClientId && auth.Token == accessToken
            //                select new
            //                {
            //                    client.ClientIdentifier,
            //                    auth.UserId,
            //                    auth.Scope,
            //                    auth.ExpirationDateUtc,
            //                    auth.CreatedOnUtc
            //                };
            //    var clientAuth = query.FirstOrDefault();
            //    if (clientAuth == null)
            //    {
            //        //throw new Exception("当前AccessToken无效，请重新认证！"); 
            //    }
            //    else if (clientAuth.ExpirationDateUtc.HasValue && clientAuth.ExpirationDateUtc < DateTime.Now)
            //    {
            //        //throw new Exception("当前AccessToken已过期！"); 
            //    } 
            //    return token;
            //}
        }
    }
    public class OAuth_ClientAuthorization
    {
        public int AuthorizationId { get; set; }

        public string Token { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public string UserId { get; set; }

        public string Scope { get; set; }

        public DateTime? ExpirationDateUtc { get; set; }

        public int ClientId { get; set; }

        //public virtual OAuth_Client Client { get; set; }
    }
}