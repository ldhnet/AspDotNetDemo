namespace WebA.RpcDemo
{
    public interface IGitHubClient
    {
        Task<string> GetData();
    }
}