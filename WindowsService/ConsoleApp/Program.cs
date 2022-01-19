// See https://aka.ms/new-console-template for more information

static void Main(string[] args)
{
    var worker = new System.ComponentModel.BackgroundWorker();

    worker.DoWork += (sender, e) =>
    {
        int sum = 0;
        for (int i = 0; i <= 10; i++)
        {
            sum += i;

            Console.WriteLine($"-----单元测试 {i} -----");
        }
        System.Threading.Thread.Sleep(30000);
    };
    worker.RunWorkerCompleted += (sender, e) =>
    {
        worker.RunWorkerAsync();
    };

    worker.RunWorkerAsync();

    Console.WriteLine("Hello, World!");
}
