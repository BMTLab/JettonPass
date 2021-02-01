using System.Threading.Tasks;


namespace JettonPass.Launcher
{
    public static class Program
    {
        public static async Task Main()
        {
            var process = ProcessManager.Start("JettonPass.exe");

            while (true)
            {
                await process!.WaitForExitAsync();
                    process.Start();
            }
        }
    }
}