namespace Lee.Hangfire
{
    public interface IJob
    {
        Task Execute();
    }
}