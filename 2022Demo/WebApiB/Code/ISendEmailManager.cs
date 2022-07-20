namespace WebApiB.Code
{
    public interface ISendEmailManager
    {
        void SendMailUsingQueue();
        void SendMailUsingChannel();
    }
}
