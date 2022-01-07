namespace DH.AuthorizationServer.Utility
{
    public interface ICustomJWTService
    {
        string GetToken(string UerName, string password);
    }
}
