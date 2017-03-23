namespace App.Models.System
{
    public class Monitor : IMonitor
    {
        public bool TryEnter(object obj, int millisecondsTimeout)
        {
            return global::System.Threading.Monitor.TryEnter(obj, millisecondsTimeout);
        }

        public void Exit(object obj)
        {
            global::System.Threading.Monitor.Exit(obj);
        }
    }
}