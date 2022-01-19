namespace minApi
{
    public class Job
    { 
        public void BackgroundWorker()
        {
            var worker = new System.ComponentModel.BackgroundWorker();

            worker.DoWork += (sender, e) =>
            {
                int sum = 0;
                for (int i = 0; i <= 100; i++)
                {
                    sum += i;

                    System.Diagnostics.Debug.WriteLine($"-----单元测试 {i} -----");
                }
                System.Threading.Thread.Sleep(3000);
            };
            worker.RunWorkerCompleted += (sender, e) =>
            {
                worker.RunWorkerAsync();
            };

            worker.RunWorkerAsync();

        }
    }
}
